using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Domain.Entities;
using System;
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
        public async Task<MediaDto> AddUserAccount(string userId, string account)
        {
            var selectedMedia =
                _mediaRepository.GetAll()
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
