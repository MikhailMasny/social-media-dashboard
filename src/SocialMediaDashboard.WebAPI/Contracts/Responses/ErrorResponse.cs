using SocialMediaDashboard.WebAPI.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    public class ErrorResponse
    {
        public List<ValidationErrorModel> Errors { get; set; } = new List<ValidationErrorModel>();
    }
}
