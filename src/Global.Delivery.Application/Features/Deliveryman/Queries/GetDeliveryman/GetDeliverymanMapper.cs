using AutoMapper;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetDeliveryman;
using Global.Delivery.Domain.Entities;

namespace Global.Motorcycle.Application.Features.Motorycycles.Queries.GetDeliveryman
{
    public class GetDeliverymanMapper : Profile
    {
        public GetDeliverymanMapper()
        {
            CreateMap<DeliverymanEntity, GetDeliverymanResponse>();
        }
    }
}
