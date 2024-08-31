using Global.Delivery.Domain.Entities;

namespace Global.Delivery.Domain.Models.Events.Deliveryman
{
    public record UpdatedDeliverymanEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string LicenseImage { get; set; }
    }
}
