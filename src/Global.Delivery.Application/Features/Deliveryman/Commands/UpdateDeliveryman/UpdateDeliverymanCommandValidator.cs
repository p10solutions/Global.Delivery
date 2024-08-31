using FluentValidation;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanCommandValidator : AbstractValidator<UpdateDeliverymanCommand>
    {
        public UpdateDeliverymanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LicenseImage)
                .NotEmpty()
                .Length(2, 200)
                .Must(BeValidImageFormat).WithMessage("Only extensions are accepted: .bmp, png");
            RuleFor(x => x.File)
                .NotEmpty()
                .When(x => x.File is not null)
                .Must(x => x.Length > 0)
                .Must(x => x.Length <= 10485760).WithMessage("Invalid size");
        }

        private bool BeValidImageFormat(string imagePath)
        {
            string[] validExtensions = { ".png", ".bmp" };
            string extension = Path.GetExtension(imagePath);

            return validExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
