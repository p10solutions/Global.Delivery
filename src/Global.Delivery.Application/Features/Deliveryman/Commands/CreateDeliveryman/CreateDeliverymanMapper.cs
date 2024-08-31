using AutoMapper;
using Global.Delivery.Domain.Entities;
using Global.Delivery.Domain.Models.Events.Deliveryman;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman
{
    public class CreateDeliverymanMapper: Profile
    {
        public CreateDeliverymanMapper()
        {
            CreateMap<CreateDeliverymanCommand, DeliverymanEntity>();
            CreateMap<DeliverymanEntity, CreateDeliverymanResponse>();
            CreateMap<DeliverymanEntity, CreatedDeliverymanEvent>();
        }
    }
}
