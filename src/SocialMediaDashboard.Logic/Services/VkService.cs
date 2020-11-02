using Microsoft.Extensions.Options;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IVkService"/>
    public class VkService : IVkService
    {
        private readonly IOptionsSnapshot<SocialNetworksSettings> _socialNetworksSettings;
        private readonly VkApi _api;

        public VkService(IOptionsSnapshot<SocialNetworksSettings> socialNetworksSettings,
                         VkApi api)
        {
            _socialNetworksSettings = socialNetworksSettings ?? throw new ArgumentNullException(nameof(socialNetworksSettings));
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<int> GetFollowersByUserNameAsync(string userName)
        {
            await _api.AuthorizeAsync(new ApiAuthParams
            {
                AccessToken = _socialNetworksSettings.Value.VkAccessToken
            });

            var response =
                (await _api.Users.GetAsync(
                    new string[]
                    {
                        userName
                    },
                    VkNet.Enums.Filters.ProfileFields.Counters))
                .FirstOrDefault()
                .Counters
                .Followers;

            return response ?? default;
        }
    }
}
