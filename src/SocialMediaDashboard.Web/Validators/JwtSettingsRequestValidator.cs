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
                .NotNull().WithMessage(ValidatorResource.JwtSecretInvalid)
                .NotEmpty().WithMessage(ValidatorResource.JwtSecretInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.JwtSecretInvalid);

            RuleFor(jwtSettingsRequest => jwtSettingsRequest.TokenLifetime)
                .NotNull().WithMessage(ValidatorResource.JwtTokenLifetimeInvalid)
                .NotEmpty().WithMessage(ValidatorResource.JwtTokenLifetimeInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.JwtTokenLifetimeInvalid);

        }
    }
}
