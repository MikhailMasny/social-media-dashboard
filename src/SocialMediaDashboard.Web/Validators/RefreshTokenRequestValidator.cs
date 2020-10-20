using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User login request validator.
    /// </summary>
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RefreshTokenRequestValidator()
        {
            RuleFor(refreshTokenRequest => refreshTokenRequest.Token)
                .NotNull().WithMessage(ValidatorResource.UserTokenRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserTokenRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserTokenRequired);

            RuleFor(refreshTokenRequest => refreshTokenRequest.RefreshToken)
                .NotNull().WithMessage(ValidatorResource.UserRefreshTokenRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserRefreshTokenRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserRefreshTokenRequired);
        }
    }
}
