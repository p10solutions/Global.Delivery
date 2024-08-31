using Global.Delivery.Domain.Entities;

namespace Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType
{
    public class GetLicenseTypeResponse
    {
        public Guid Id { get; init; }
        public ELicenseType LicenseType { get; set; }
    }
}
