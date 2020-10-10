using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;
using System.Globalization;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// User registration request validator.
    /// </summary>
    public class UserRegistrationRequestValidator : AbstractValidator<UserSignUpRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserRegistrationRequestValidator()
        {
            const int usernameMinimumLength = 5;
            const int usernameMaximumLength = 50;

            RuleFor(userRegistrationRequest => userRegistrationRequest.Email)
                .NotEmpty()
                .WithMessage(ValidatorResource.EmailRequired)
                .EmailAddress()
                .WithMessage(ValidatorResource.EmailInvalid);

            RuleFor(userRegistrationRequest => userRegistrationRequest.UserName)
                .NotEmpty()
                .WithMessage(ValidatorResource.UsernameRequired)
                .Length(usernameMinimumLength, usernameMaximumLength)
                .WithMessage(string.Format(CultureInfo.InvariantCulture, ValidatorResource.UsernameShort, usernameMinimumLength, usernameMaximumLength));

            RuleFor(userRegistrationRequest => userRegistrationRequest.Password)
                .NotEmpty()
                .WithMessage(ValidatorResource.PasswordRequired);
        }
    }
}
