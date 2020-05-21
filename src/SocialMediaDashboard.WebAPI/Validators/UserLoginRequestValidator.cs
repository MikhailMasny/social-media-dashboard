using FluentValidation;
using SocialMediaDashboard.WebAPI.Contracts.Requests;

namespace SocialMediaDashboard.WebAPI.Validators
{
    /// <summary>
    /// User login request validator.
    /// </summary>
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
