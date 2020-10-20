using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Subscription request validator.
    /// </summary>
    public class SubscriptionCreateOrUpdateRequestValidator : AbstractValidator<SubscriptionCreateOrUpdateRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SubscriptionCreateOrUpdateRequestValidator()
        {
            RuleFor(subscriptionCreateOrUpdateRequest => subscriptionCreateOrUpdateRequest.AccountName)
                .NotNull().WithMessage(ValidatorResource.SubscriptionAccountNameRequired)
                .NotEmpty().WithMessage(ValidatorResource.SubscriptionAccountNameRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.SubscriptionAccountNameInvalid);

            RuleFor(subscriptionCreateOrUpdateRequest => subscriptionCreateOrUpdateRequest.SubscriptionTypeId)
                .NotEqual(default(int)).WithMessage(ValidatorResource.SubscriptionTypeIdInvalid);
        }
    }
}
