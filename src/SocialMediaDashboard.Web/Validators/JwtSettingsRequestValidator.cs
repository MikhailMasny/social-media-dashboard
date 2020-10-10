using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Jwt settings request validator.
    /// </summary>
    public class JwtSettingsRequestValidator : AbstractValidator<JwtSettingsRequest>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JwtSettingsRequestValidator()
        {
            RuleFor(jwtSettingsRequest => jwtSettingsRequest.Secret)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.JwtSecretInvalid);

            RuleFor(jwtSettingsRequest => jwtSettingsRequest.TokenLifetime)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.JwtTokenLifetimeInvalid);

        }
    }
}
