using FluentValidation;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman
{
    public class CreateDeliverymanCommandValidator : AbstractValidator<CreateDeliverymanCommand>
    {
        public CreateDeliverymanCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(2, 200);
            RuleFor(x => x.LicenseNumber).NotEmpty();
            RuleFor(x=>x.LicenseNumber).Length(2, 20);
            RuleFor(x => x.Document).NotEmpty();
            RuleFor(x => x.Document).Length(2, 20);
            RuleFor(x => x.LicenseType).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
        }
    }
}
