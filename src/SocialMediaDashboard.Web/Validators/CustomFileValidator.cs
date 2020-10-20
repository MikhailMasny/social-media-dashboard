using FluentValidation;
using Microsoft.AspNetCore.Http;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;

namespace SocialMediaDashboard.Web.Validators
{
    /// <summary>
    /// Custom validator for IFormFile.
    /// </summary>
    public class CustomFileValidator : AbstractValidator<IFormFile>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomFileValidator()
        {
            RuleFor(formFile => formFile.Length)
                .NotNull()
                .LessThanOrEqualTo(ValidatorConstant.FileMaximumLength)
                .WithMessage(ValidatorResource.FileMaximumLength);

            RuleFor(formFile => formFile.ContentType)
                .NotNull()
                .Must(BeAValidType)
                .WithMessage(ValidatorResource.FileTypeNotAllowed);
        }

        private bool BeAValidType(string type)
        {
            return type == ValidatorConstant.FileJpegFormat
                || type == ValidatorConstant.FileJpgFormat
                || type == ValidatorConstant.FilePngFormat;
        }
    }
}
