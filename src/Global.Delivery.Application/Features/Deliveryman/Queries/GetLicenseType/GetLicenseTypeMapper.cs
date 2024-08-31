using AutoMapper;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType;
using Global.Delivery.Domain.Entities;

namespace Global.Motorcycle.Application.Features.Motorycycles.Queries.GetLicenseType
{
    public class GetLicenseTypeMapper : Profile
    {
        public GetLicenseTypeMapper()
        {
            CreateMap<DeliverymanEntity, GetLicenseTypeResponse>();
        }
    }
}
