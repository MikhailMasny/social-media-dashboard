using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Social networks settings request validator.
    /// </summary>
    public class SocialNetworksSettingsRequestValidator : AbstractValidator<SocialNetworksSettingsRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SocialNetworksSettingsRequestValidator()
        {
            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.VkAccessToken)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SocialNetworkVkAccessTokenInvalid);

            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.InstagramAccount.Username)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SocialNetworkInstagramAccountUsernameInvalid);

            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.InstagramAccount.Password)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SocialNetworkInstagramAccountPasswordInvalid);

            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.YouTubeAccessToken)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SocialNetworkYouTubeAccessTokenInvalid);
        }
    }
}
