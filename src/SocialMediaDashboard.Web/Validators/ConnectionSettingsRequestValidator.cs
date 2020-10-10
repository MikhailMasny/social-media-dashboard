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
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.ConnectionMsSqlServerInvalid);

            RuleFor(connectionSettingsRequest => connectionSettingsRequest.PostgreSqlConnection)
                .NotNull()
                .NotEmpty()
                .NotEqual(CommonResource.String)
                .WithMessage(ValidatorResource.ConnectionPostgreSqlInvalid);
        }
    }
}
