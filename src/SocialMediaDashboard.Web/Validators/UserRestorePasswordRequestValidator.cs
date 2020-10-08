using FluentValidation;
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
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
        }
    }
}
