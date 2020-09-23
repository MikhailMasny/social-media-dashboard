using Flurl;
using Flurl.Http;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Models;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IRequestService"/>
    public class RequestService : IRequestService
    {
        private readonly string _youTubeApi = "https://www.googleapis.com";

        public async Task<YouTubeResult> GetDataByChannelFromYouTubeApiAsync(string channel)
        {
            return await _youTubeApi
                .AppendPathSegments("youtube", "v3", "channels")
                .SetQueryParams(new
                {
                    part = "statistics",
                    id = channel,
                    key = "key"
                })
                .GetJsonAsync<YouTubeResult>();
        }
    }
}
