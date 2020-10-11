using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Queries;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Email query validator.
    /// </summary>
    public class EmailQueryValidator : AbstractValidator<EmailQuery>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailQueryValidator()
        {
            RuleFor(emailQuery => emailQuery.Email)
                .NotNull().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserEmailRequired)
                .EmailAddress().WithMessage(ValidatorResource.UserEmailInvalid);

            RuleFor(emailQuery => emailQuery.Code)
                .NotNull().WithMessage(ValidatorResource.UserVerifyCodeRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserVerifyCodeRequired);
        }
    }
}
