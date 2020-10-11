using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Requests;
using System;
using System.Globalization;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Profile request validator.
    /// </summary>
    public class ProfileUpdateRequestValidator : AbstractValidator<ProfileUpdateRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ProfileUpdateRequestValidator()
        {
            RuleFor(profileUpdateRequest => profileUpdateRequest.Name)
                .NotNull().WithMessage(ValidatorResource.UserNameRequired)
                .NotEmpty().WithMessage(ValidatorResource.UserNameRequired)
                .Length(ValidatorConstant.UserNameMinimumLength, ValidatorConstant.UserNameMaximumLength)
                .WithMessage(string.Format(CultureInfo.InvariantCulture,
                    ValidatorResource.UserNameShort,
                    ValidatorConstant.UserNameMinimumLength,
                    ValidatorConstant.UserNameMaximumLength));

            RuleFor(profileUpdateRequest => profileUpdateRequest.Gender)
                .IsInEnum().WithMessage(ValidatorResource.ProfileGenderInvalid);

            RuleFor(profileUpdateRequest => profileUpdateRequest.BirthDate)
                .GreaterThanOrEqualTo(DateTime.UnixEpoch).WithMessage(ValidatorResource.ProfileBirthDateInvalid);

            RuleFor(profileUpdateRequest => profileUpdateRequest.Avatar)
                .SetValidator(new CustomFileValidator());
        }
    }
}
