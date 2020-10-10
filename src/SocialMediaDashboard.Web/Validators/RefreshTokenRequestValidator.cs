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
                .NotEmpty()
                .WithMessage(ValidatorResource.TokenRequired);

            RuleFor(refreshTokenRequest => refreshTokenRequest.RefreshToken)
                .NotEmpty()
                .WithMessage(ValidatorResource.RefreshTokenRequired);
        }
    }
}
