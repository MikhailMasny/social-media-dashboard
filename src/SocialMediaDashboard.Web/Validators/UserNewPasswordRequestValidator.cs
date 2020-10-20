using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User new password request validator.
    /// </summary>
    public class UserNewPasswordRequestValidator : AbstractValidator<UserNewPasswordRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserNewPasswordRequestValidator()
        {
            RuleFor(userNewPasswordRequest => userNewPasswordRequest.Password)
                .NotNull().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserPasswordRequired);

            RuleFor(userNewPasswordRequest => userNewPasswordRequest.ConfirmPassword)
                .NotNull().WithMessage(ValidatorResource.UserConfirmPasswordRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserConfirmPasswordRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserConfirmPasswordRequired);
        }
    }
}
