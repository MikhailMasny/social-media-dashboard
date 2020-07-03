using Microsoft.EntityFrameworkCore;
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

        public MediaService(IRepository<Media> mediaRepository)
        {
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MediaDto>> GetAllAccounts()
        {
            var allMedia = await _mediaRepository.GetAll()
                .ToListAsync();

            // TODO: Use Automapper
            var mediaDtos = new List<MediaDto>();
            foreach (var media in allMedia)
            {
                var m = new MediaDto
                {
                    Id = media.Id,
                    AccountName = media.AccountName,
                    UserId = media.UserId
                };
                mediaDtos.Add(m);
            }

            return mediaDtos;
        }

        /// <inheritdoc/>
        public async Task<MediaDto> AddUserAccount(string userId, string account)
        {
            var selectedMedia = _mediaRepository.GetAll()
                .Where(m => m.UserId == userId)
                .FirstOrDefault(m => m.AccountName == account);

            if (selectedMedia != null)
            {
                return null;
            }

            var media = new Media
            {
                AccountName = account,
                UserId = userId
            };

            await _mediaRepository.AddAsync(media);
            await _mediaRepository.SaveChangesAsync();

            var mediaDto = new MediaDto
            {
                AccountName = media.AccountName,
                UserId = media.UserId
            };

            return mediaDto;
        }
    }
}
