using Confluent.Kafka;
using Global.Delivery.Domain.Contracts.Events;
using Global.Delivery.Domain.Models.Events.Deliveryman;
using Global.Delivery.Infraestructure.Events.Serializer;
using Microsoft.Extensions.Configuration;

namespace Global.Delivery.Infraestructure.Events.Deliveryman
{
    public class DeliverymanProducer : IDeliverymanProducer
    {
        readonly ProducerConfig _config;
        readonly string _topicCreated;
        readonly string _topicUpdated;

        public DeliverymanProducer(IConfiguration configuration)
        {
            _topicCreated = configuration.GetSection("Kafka:CreateTopic").Value;
            _topicUpdated = configuration.GetSection("Kafka:UpdateTopic").Value;
            _config = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
            };
        }

        public async Task SendCreatedEventAsync(CreatedDeliverymanEvent @event)
        {
            using var producer = new ProducerBuilder<Guid, CreatedDeliverymanEvent>(_config)
                .SetKeySerializer(new GuidSerializer())
                .SetValueSerializer(new CreatedDeliverymanEventSerializer())
                .Build();

            var message = new Message<Guid, CreatedDeliverymanEvent>() { Key = @event.Id, Value = @event };

            await producer.ProduceAsync(_topicCreated, message);
        }

        public async Task SendUpdatedEventAsync(UpdatedDeliverymanEvent @event)
        {
            using var producer = new ProducerBuilder<Guid, UpdatedDeliverymanEvent>(_config)
              .SetKeySerializer(new GuidSerializer())
              .SetValueSerializer(new UpdatedDeliverymanEventSerializer())
              .Build();

            var message = new Message<Guid, UpdatedDeliverymanEvent>() { Key = @event.Id, Value = @event };

            await producer.ProduceAsync(_topicUpdated, message);
        }
    }
}
