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
                .NotNull().WithMessage(ValidatorResource.SocialNetworkVkAccessTokenInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SocialNetworkVkAccessTokenInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SocialNetworkVkAccessTokenInvalid);

            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.InstagramAccount.Username)
                .NotNull().WithMessage(ValidatorResource.SocialNetworkInstagramAccountUsernameInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SocialNetworkInstagramAccountUsernameInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SocialNetworkInstagramAccountUsernameInvalid);

            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.InstagramAccount.Password)
                .NotNull().WithMessage(ValidatorResource.SocialNetworkInstagramAccountPasswordInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SocialNetworkInstagramAccountPasswordInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SocialNetworkInstagramAccountPasswordInvalid);

            RuleFor(socialNetworksSettingsRequest => socialNetworksSettingsRequest.YouTubeAccessToken)
                .NotNull().WithMessage(ValidatorResource.SocialNetworkYouTubeAccessTokenInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SocialNetworkYouTubeAccessTokenInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SocialNetworkYouTubeAccessTokenInvalid);
        }
    }
}
