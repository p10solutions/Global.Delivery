using Global.Delivery.Application.Features.Common;
using Global.Delivery.Domain.Entities;
using MediatR;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman
{
    public class CreateDeliverymanCommand(string name, string document, DateTime dateOfBirth, string licenseNumber, ELicenseType licenseType)
        : CommandBase<CreateDeliverymanCommand>(new CreateDeliverymanCommandValidator()), IRequest<CreateDeliverymanResponse>
    {
        public string Name { get; set; } = name;
        public string Document { get; set; } = document;
        public DateTime DateOfBirth { get; set; } = dateOfBirth;
        public string LicenseNumber { get; set; } = licenseNumber;
        public ELicenseType LicenseType { get; set; } = licenseType;
    }
}
