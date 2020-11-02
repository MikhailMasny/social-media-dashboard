using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Mail settings request validator.
    /// </summary>
    public class MailSettingsRequestValidator : AbstractValidator<MailSettingsRequest>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MailSettingsRequestValidator()
        {
            RuleFor(mailSettingsRequest => mailSettingsRequest.Server)
                .NotNull().WithMessage(ValidatorResource.MailServerInvalid)
                .NotEmpty().WithMessage(ValidatorResource.MailServerInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.MailServerInvalid);

            RuleFor(mailSettingsRequest => mailSettingsRequest.Port)
                .Must(BeAValidPort).WithMessage(ValidatorResource.MailPortInvalid);

            RuleFor(mailSettingsRequest => mailSettingsRequest.Address)
                .NotNull().WithMessage(ValidatorResource.MailAddressInvalid)
                .NotEmpty().WithMessage(ValidatorResource.MailAddressInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.MailAddressInvalid);

            RuleFor(mailSettingsRequest => mailSettingsRequest.Password)
                .NotNull().WithMessage(ValidatorResource.MailPasswordInvalid)
                .NotEmpty().WithMessage(ValidatorResource.MailPasswordInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.MailPasswordInvalid);
        }

        private bool BeAValidPort(int port)
        {
            return port switch
            {
                25 => true,
                465 => true,
                587 => true,
                _ => false,
            };
        }
    }
}
