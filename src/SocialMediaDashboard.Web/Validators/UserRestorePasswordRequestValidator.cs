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
                .NotEmpty()
                .WithMessage(ValidatorResource.EmailRequired)
                .EmailAddress()
                .WithMessage(ValidatorResource.EmailInvalid);
        }
    }
}
