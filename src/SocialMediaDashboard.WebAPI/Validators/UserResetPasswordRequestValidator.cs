using FluentValidation;
using SocialMediaDashboard.WebAPI.Contracts.Requests;

namespace SocialMediaDashboard.WebAPI.Validators
{
    /// <summary>
    /// User reset password request validator.
    /// </summary>
    public class UserResetPasswordRequestValidator : AbstractValidator<UserResetPasswordRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Password is required.");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Verify code is required.");
        }
    }
}
