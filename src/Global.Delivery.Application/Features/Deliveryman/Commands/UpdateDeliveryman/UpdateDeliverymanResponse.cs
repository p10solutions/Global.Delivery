using Global.Delivery.Domain.Entities;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanResponse(Guid id, string name, string document, DateTime dateOfBirth, string licenseNumber, ELicenseType licenseType, string? licenseImage)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Document { get; set; } = document;
        public DateTime DateOfBirth { get; set; } = dateOfBirth;
        public string LicenseNumber { get; set; } = licenseNumber;
        public ELicenseType LicenseType { get; set; } = licenseType;
        public string? LicenseImage { get; set; } = licenseImage;
    }
}
