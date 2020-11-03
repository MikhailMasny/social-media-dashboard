using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Constants;
using SocialMediaDashboard.Domain.Helpers;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IRequestService"/>
    public class RequestService : IRequestService
    {
        private readonly IOptionsSnapshot<SocialNetworksSettings> _socialNetworksSettings;

        public RequestService(IOptionsSnapshot<SocialNetworksSettings> socialNetworksSettings)
        {
            _socialNetworksSettings = socialNetworksSettings ?? throw new ArgumentNullException(nameof(socialNetworksSettings));
        }

        public async Task<YouTubeResult> GetDataFromYouTubeApiByChannelAsync(string channel)
        {
            return await ApiPlatform.YouTube.Domain
                .AppendPathSegments(
                    ApiPlatform.YouTube.MainPath,
                    ApiPlatform.YouTube.Version,
                    ApiPlatform.YouTube.ChannelType)
                .SetQueryParams(new
                {
                    part = ApiPlatform.YouTube.StatisticPart,
                    id = channel,
                    key = _socialNetworksSettings.Value.YouTubeAccessToken,
                })
                .GetJsonAsync<YouTubeResult>();
        }

        public async Task<YouTubeResult> GetDataFromYouTubeApiByUsernameAsync(string username)
        {
            return await ApiPlatform.YouTube.Domain
                .AppendPathSegments(
                    ApiPlatform.YouTube.MainPath,
                    ApiPlatform.YouTube.Version,
                    ApiPlatform.YouTube.ChannelType)
                .SetQueryParams(new
                {
                    part = ApiPlatform.YouTube.StatisticPart,
                    forUsername = username,
                    key = _socialNetworksSettings.Value.YouTubeAccessToken,
                })
                .GetJsonAsync<YouTubeResult>();
        }

        public async Task<YouTubeResult> GetDataFromYouTubeApiByVideoAsync(string video)
        {
            return await ApiPlatform.YouTube.Domain
                .AppendPathSegments(
                    ApiPlatform.YouTube.MainPath,
                    ApiPlatform.YouTube.Version,
                    ApiPlatform.YouTube.VideoType)
                .SetQueryParams(new
                {
                    part = ApiPlatform.YouTube.StatisticPart,
                    id = video,
                    key = _socialNetworksSettings.Value.YouTubeAccessToken,
                })
                .GetJsonAsync<YouTubeResult>();
        }
    }
}
