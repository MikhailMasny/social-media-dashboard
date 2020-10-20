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
                .NotNull().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserEmailRequired)
                .EmailAddress().WithMessage(ValidatorResource.UserEmailInvalid);

            RuleFor(userResetPasswordRequest => userResetPasswordRequest.NewPassword)
                .NotNull().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserPasswordRequired);

            RuleFor(userResetPasswordRequest => userResetPasswordRequest.Code)
                .NotNull().WithMessage(ValidatorResource.UserVerifyCodeRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserVerifyCodeRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserVerifyCodeRequired);
        }
    }
}
