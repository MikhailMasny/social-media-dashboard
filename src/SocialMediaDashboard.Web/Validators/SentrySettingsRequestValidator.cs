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
                .NotNull().WithMessage(ValidatorResource.SentryDsnInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SentryDsnInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SentryDsnInvalid);

            RuleFor(sentrySettingsRequest => sentrySettingsRequest.MinimumBreadcrumbLevel)
                .NotNull().WithMessage(ValidatorResource.SentryMinimumBreadcrumbLevelInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SentryMinimumBreadcrumbLevelInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SentryMinimumBreadcrumbLevelInvalid);

            RuleFor(sentrySettingsRequest => sentrySettingsRequest.MinimumEventLevel)
                .NotNull().WithMessage(ValidatorResource.SentryMinimumEventLevelInvalid)
                .NotEmpty().WithMessage(ValidatorResource.SentryMinimumEventLevelInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SentryMinimumEventLevelInvalid);
        }
    }
}
