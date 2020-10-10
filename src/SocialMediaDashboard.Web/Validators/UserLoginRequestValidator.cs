using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User login request validator.
    /// </summary>
    public class UserLoginRequestValidator : AbstractValidator<UserSignInRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserLoginRequestValidator()
        {
            RuleFor(userLoginRequest => userLoginRequest.Email)
                .NotEmpty()
                .WithMessage(ValidatorResource.EmailRequired)
                .EmailAddress()
                .WithMessage(ValidatorResource.EmailInvalid);

            RuleFor(userLoginRequest => userLoginRequest.Password)
                .NotEmpty()
                .WithMessage(ValidatorResource.PasswordRequired);
        }
    }
}
