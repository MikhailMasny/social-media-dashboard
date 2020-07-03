using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Domain.Models;
using System;
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
        public async Task AddUserAccount(string userId, string account)
        {
            var media = new Media
            {
                AccountName = account,
                UserId = userId
            };

            await _mediaRepository.AddAsync(media);
            await _mediaRepository.SaveChangesAsync();
        }
    }
}
