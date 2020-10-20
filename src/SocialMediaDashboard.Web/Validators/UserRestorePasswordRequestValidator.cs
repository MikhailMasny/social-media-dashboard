using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User restore password request validator
    /// </summary>
    public class UserRestorePasswordRequestValidator : AbstractValidator<UserRestorePasswordRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserRestorePasswordRequestValidator()
        {
            RuleFor(userRestorePasswordRequest => userRestorePasswordRequest.Email)
                .NotNull().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserEmailRequired)
                .EmailAddress().WithMessage(ValidatorResource.UserEmailInvalid);
        }
    }
}
