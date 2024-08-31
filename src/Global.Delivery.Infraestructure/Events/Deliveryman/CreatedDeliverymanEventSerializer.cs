using Confluent.Kafka;
using Global.Delivery.Domain.Models.Events.Deliveryman;
using System.Text;
using System.Text.Json;

namespace Global.Delivery.Infraestructure.Events.Deliveryman
{
    public class CreatedDeliverymanEventSerializer : IAsyncSerializer<CreatedDeliverymanEvent>
    {
        public Task<byte[]> SerializeAsync(CreatedDeliverymanEvent data, SerializationContext context)
        {
            var json = JsonSerializer.Serialize(data);
            return Task.FromResult(Encoding.ASCII.GetBytes(json));
        }
    }
}
