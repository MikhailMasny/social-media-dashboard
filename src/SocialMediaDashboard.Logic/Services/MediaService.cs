using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IMediaService"/>
    public class MediaService : IMediaService
    {
        private readonly IRepository<Media> _mediaRepository;
        private readonly IMapper _mapper;

        public MediaService(IRepository<Media> mediaRepository,
                            IMapper mapper)
        {
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<bool> AddAccountAsync(string userId, string account, AccountType accountType)
        {
            var canCreateMedia = await CanUserCreateMedia(userId, account, accountType);
            if (!canCreateMedia)
            {
                return false;
            }

            var media = new Media
            {
                AccountName = account,
                Type = accountType,
                UserId = userId
            };

            await _mediaRepository.AddAsync(media);
            await _mediaRepository.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MediaDto>> GetAllUserAccountsAsync(string userId)
        {
            var media = await _mediaRepository.GetAllWithoutTracking()
                .Where(m => m.UserId == userId)
                .ToListAsync();

            var mediaDto = _mapper.Map<List<MediaDto>>(media);

            return mediaDto;
        }

        /// <inheritdoc/>
        public async Task<MediaDto> GetAccountAsync(int id)
        {
            var media = await _mediaRepository.GetAllWithoutTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            var mediaDto = _mapper.Map<MediaDto>(media);

            return mediaDto;
        }

        /// <inheritdoc/>
        public async Task<bool> AccountExistAsync(int id)
        {
            var media = await _mediaRepository.GetEntityAsync(m => m.Id == id);
            if (media == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CanUserCreateMedia(string userId, string account, AccountType accountType)
        {
            var selectedMedia = await _mediaRepository.GetAllWithoutTracking()
                .Where(m => m.UserId == userId)
                .FirstOrDefaultAsync(m => m.AccountName == account && m.Type == accountType);

            if (selectedMedia != null)
            {
                return false;
            }

            return true;
        }
    }
}
