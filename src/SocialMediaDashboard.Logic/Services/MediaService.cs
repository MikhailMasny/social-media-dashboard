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
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public MediaService(IRepository<Media> mediaRepository,
                            IRepository<Subscription> subscriptionRepository,
                            IMapper mapper)
        {
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MediaDto>> GetAllAccounts()
        {
            var media = await _mediaRepository.GetAll()
                .ToListAsync();

            var mediaDtos = _mapper.Map<List<MediaDto>>(media);

            return mediaDtos;
        }

        /// <inheritdoc/>
        public async Task<MediaDto> GetAccount(int id)
        {
            var media = await _mediaRepository.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);

            // TODO: Try to fix it
            //var mediaDto = _mapper.Map<MediaDto>(media);

            var mediaDto = new MediaDto
            {
                Id = media.Id,
                AccountName = media.AccountName,
                Type = media.Type,
                UserId = media.UserId
            };

            return mediaDto;
        }

        // TODO: replace to SubService
        /// <inheritdoc/>
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByType(AccountType accountType, SubscriptionType subscriptionType)
        {
            var subscriptions = await _subscriptionRepository.GetAll()
                .Include(s => s.Media)
                .Where(s => s.Type == subscriptionType && s.Media.Type == accountType)
                .ToListAsync();

            var subscriptionDtos = new List<SubscriptionDto>();
            foreach (var subscription in subscriptions)
            {
                var subDto = new SubscriptionDto
                {
                    Id = subscription.Id,
                    IsDisplayed = subscription.IsDisplayed,
                    Type = subscription.Type,
                    MediaId = subscription.MediaId
                };

                subscriptionDtos.Add(subDto);
            }

            // TODO: Try to fix it
            //var subscriptionDtos = _mapper.Map<List<SubscriptionDto>>(subscriptions); 

            return subscriptionDtos;
        }

        /// <inheritdoc/>
        public async Task<bool> AddUserAccount(string userId, string account, AccountType accountType, SubscriptionType subscriptionType)
        {
            var canCreateMedia = await CanUserCreateMedia(userId, account, accountType);
            if (!canCreateMedia)
            {
                return false;
            }

            var canCreateSubscription = await CanUserCreateSubscription(userId, account, accountType, subscriptionType);
            if (!canCreateSubscription)
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

            var subscription = new Subscription
            {
                Type = subscriptionType,
                IsDisplayed = true,
                MediaId = media.Id
            };

            await _subscriptionRepository.AddAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return true;
        }

        private async Task<bool> CanUserCreateMedia(string userId, string account, AccountType accountType)
        {
            var selectedMedia = await _mediaRepository.GetAll()
                .Where(m => m.UserId == userId)
                .FirstOrDefaultAsync(m => m.AccountName == account && m.Type == accountType);

            if (selectedMedia != null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CanUserCreateSubscription(string userId, string account, AccountType accountType, SubscriptionType subscriptionType)
        {
            var selectedSubscriptions = await _subscriptionRepository.GetAll()
                .Include(s => s.Media)
                .Where(s => s.Media.UserId == userId && s.Media.AccountName == account && s.Media.Type == accountType)
                .ToListAsync();

            foreach (var subscription in selectedSubscriptions)
            {
                if (subscription.Type == subscriptionType)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
