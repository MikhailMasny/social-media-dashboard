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

        public VkService(IOptionsSnapshot<SocialNetworksSettings> socialNetworksSettings)
        {
            _socialNetworksSettings = socialNetworksSettings ?? throw new ArgumentNullException(nameof(socialNetworksSettings));
        }

        public async Task<int> GetFollowersByUserNameAsync(string userName)
        {
            using var vkApi = new VkApi();
            await vkApi.AuthorizeAsync(new ApiAuthParams
            {
                AccessToken = _socialNetworksSettings.Value.VkAccessToken
            });

            var response =
                (await vkApi.Users.GetAsync(
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
