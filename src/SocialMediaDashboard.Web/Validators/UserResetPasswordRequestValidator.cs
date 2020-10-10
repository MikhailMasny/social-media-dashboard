using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
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
            RuleFor(userResetPasswordRequest => userResetPasswordRequest.Email)
                .NotEmpty()
                .WithMessage(ValidatorResource.EmailRequired)
                .EmailAddress()
                .WithMessage(ValidatorResource.EmailInvalid);

            RuleFor(userResetPasswordRequest => userResetPasswordRequest.NewPassword)
                .NotEmpty()
                .WithMessage(ValidatorResource.PasswordRequired);

            RuleFor(userResetPasswordRequest => userResetPasswordRequest.Code)
                .NotEmpty()
                .WithMessage(ValidatorResource.VerifyCodeRequired);
        }
    }
}
