using System.Collections.Generic;

namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Page info.
    /// </summary>
    public class PageInfo
    {
        public int ResultsPerPage { get; set; }
    }

    /// <summary>
    /// Statistics.
    /// </summary>
    public class Statistics
    {
        public string ViewCount { get; set; }
        public string CommentCount { get; set; }
        public string SubscriberCount { get; set; }
        public bool HiddenSubscriberCount { get; set; }
        public string VideoCount { get; set; }
    }

    /// <summary>
    /// Item.
    /// </summary>
    public class Item
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public Statistics Statistics { get; set; }
    }

    /// <summary>
    /// YouTube response result.
    /// </summary>
    public class YouTubeResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public PageInfo PageInfo { get; set; }
        public IList<Item> Items { get; set; }
    }
}
