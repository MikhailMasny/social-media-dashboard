using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User login request validator.
    /// </summary>
    public class UserSignInRequestValidator : AbstractValidator<UserSignInRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserSignInRequestValidator()
        {
            RuleFor(userLoginRequest => userLoginRequest.Email)
                .NotNull().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserEmailRequired)
                .EmailAddress().WithMessage(ValidatorResource.UserEmailInvalid);

            RuleFor(userLoginRequest => userLoginRequest.Password)
                .NotNull().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserPasswordRequired);
        }
    }
}
