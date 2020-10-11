using FluentValidation;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Contracts.Requests;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Connection settings request validator.
    /// </summary>
    public class ConnectionSettingsRequestValidator : AbstractValidator<ConnectionSettingsRequest>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ConnectionSettingsRequestValidator()
        {
            RuleFor(connectionSettingsRequest => connectionSettingsRequest.MsSqlServerConnection)
                .NotNull().WithMessage(ValidatorResource.ConnectionMsSqlServerInvalid)
                .NotEmpty().WithMessage(ValidatorResource.ConnectionMsSqlServerInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.ConnectionMsSqlServerInvalid);

            RuleFor(connectionSettingsRequest => connectionSettingsRequest.PostgreSqlConnection)
                .NotNull().WithMessage(ValidatorResource.ConnectionPostgreSqlInvalid)
                .NotEmpty().WithMessage(ValidatorResource.ConnectionPostgreSqlInvalid)
                .NotEqual(CommonResource.String).WithMessage(ValidatorResource.ConnectionPostgreSqlInvalid);
        }
    }
}
