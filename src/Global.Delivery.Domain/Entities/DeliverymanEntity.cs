namespace Global.Delivery.Domain.Entities
{
    public class DeliverymanEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string? LicenseImage { get; set; }

        public DeliverymanEntity(string name, string document, DateTime dateOfBirth, string licenseNumber, ELicenseType licenseType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Document = document;
            DateOfBirth = dateOfBirth;
            LicenseNumber = licenseNumber;
            LicenseType = licenseType;
        }
    }
}
