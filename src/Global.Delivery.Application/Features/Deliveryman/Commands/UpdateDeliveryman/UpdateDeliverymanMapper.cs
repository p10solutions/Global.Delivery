using AutoMapper;
using Global.Delivery.Domain.Entities;
using Global.Delivery.Domain.Models.Events.Deliveryman;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanMapper : Profile
    {
        public UpdateDeliverymanMapper()
        {
            CreateMap<UpdateDeliverymanCommand, DeliverymanEntity>();
            CreateMap<DeliverymanEntity, UpdateDeliverymanResponse>();
            CreateMap<DeliverymanEntity, UpdatedDeliverymanEvent>();
        }
    }
}
