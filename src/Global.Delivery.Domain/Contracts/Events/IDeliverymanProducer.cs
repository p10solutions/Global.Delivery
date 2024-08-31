using Global.Delivery.Domain.Models.Events.Deliveryman;

namespace Global.Delivery.Domain.Contracts.Events
{
    public interface IDeliverymanProducer
    {
        Task SendCreatedEventAsync(CreatedDeliverymanEvent @event);
        Task SendUpdatedEventAsync(UpdatedDeliverymanEvent @event);
    }
}
