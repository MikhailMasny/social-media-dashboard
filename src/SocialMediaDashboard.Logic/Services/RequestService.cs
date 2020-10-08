using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Helpers;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IRequestService"/>
    public class RequestService : IRequestService
    {
        private readonly IOptionsSnapshot<SocialNetworksSettings> _socialNetworksSettings;
        private readonly string _youTubeApi = "https://www.googleapis.com";

        public RequestService(IOptionsSnapshot<SocialNetworksSettings> socialNetworksSettings)
        {
            _socialNetworksSettings = socialNetworksSettings ?? throw new ArgumentNullException(nameof(socialNetworksSettings));
        }

        public async Task<YouTubeResult> GetDataByChannelFromYouTubeApiAsync(string channel)
        {
            return await _youTubeApi
                .AppendPathSegments("youtube", "v3", "channels")
                .SetQueryParams(new
                {
                    part = "statistics",
                    id = channel,
                    key = _socialNetworksSettings.Value.YouTubeAccessToken
                })
                .GetJsonAsync<YouTubeResult>();
        }
    }
}
