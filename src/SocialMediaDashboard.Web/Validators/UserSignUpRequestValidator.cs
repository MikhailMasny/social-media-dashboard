using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Requests;
using System.Globalization;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User registration request validator.
    /// </summary>
    public class UserSignUpRequestValidator : AbstractValidator<UserSignUpRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserSignUpRequestValidator()
        {
            RuleFor(userRegistrationRequest => userRegistrationRequest.Email)
                .NotNull().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserEmailRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserEmailRequired)
                .EmailAddress().WithMessage(ValidatorResource.UserEmailInvalid);

            RuleFor(userRegistrationRequest => userRegistrationRequest.Password)
                .NotNull().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserPasswordRequired)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.UserPasswordRequired);

            RuleFor(userRegistrationRequest => userRegistrationRequest.Name)
                .NotNull().WithMessage(ValidatorResource.UserNameRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserNameRequired)
                .Length(ValidatorConstant.UserNameMinimumLength, ValidatorConstant.UserNameMaximumLength)
                .WithMessage(string.Format(CultureInfo.InvariantCulture,
                    ValidatorResource.UserNameShort,
                    ValidatorConstant.UserNameMinimumLength,
                    ValidatorConstant.UserNameMaximumLength));
        }
    }
}
