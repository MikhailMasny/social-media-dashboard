using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Options;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Helpers;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IInstagramService"/>
    public class InstagramService : IInstagramService
    {
        private readonly IOptionsSnapshot<SocialNetworksSettings> _socialNetworksSettings;

        public InstagramService(IOptionsSnapshot<SocialNetworksSettings> socialNetworksSettings)
        {
            _socialNetworksSettings = socialNetworksSettings ?? throw new ArgumentNullException(nameof(socialNetworksSettings));
        }

        // TODO: fix it to int with string (answer)
        public async Task<int> GetFollowersByUserNameAsync(string userName)
        {
            var userSession = new UserSessionData
            {
                UserName = _socialNetworksSettings.Value.InstagramAccount.Username,
                Password = _socialNetworksSettings.Value.InstagramAccount.Password
            };

            var instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .Build();

            if (!instaApi.IsUserAuthenticated)
            {
                // TODO: delete it
                Console.WriteLine($"Logging in as {userSession.UserName}");
                var logInResult = await instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                {
                    Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                    return default;
                }
            }

            var userInfo = await instaApi.UserProcessor.GetUserInfoByUsernameAsync(userName);
            var followersCount = (int)userInfo.Value.FollowerCount;
            return followersCount;
        }
    }
}
