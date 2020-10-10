using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Sentry settings request validator.
    /// </summary>
    public class SentrySettingsRequestValidator : AbstractValidator<SentrySettingsRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SentrySettingsRequestValidator()
        {
            RuleFor(sentrySettingsRequest => sentrySettingsRequest.Dsn)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SentryDsnInvalid);

            RuleFor(sentrySettingsRequest => sentrySettingsRequest.MinimumBreadcrumbLevel)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SentryMinimumBreadcrumbLevelInvalid);

            RuleFor(sentrySettingsRequest => sentrySettingsRequest.MinimumEventLevel)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.SentryMinimumEventLevelInvalid);
        }
    }
}
