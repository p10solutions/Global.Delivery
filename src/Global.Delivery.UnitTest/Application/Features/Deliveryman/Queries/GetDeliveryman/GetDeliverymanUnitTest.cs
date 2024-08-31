using AutoFixture;
using AutoMapper;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetDeliveryman;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Entities;
using Global.Delivery.Domain.Models.Notifications;
using Global.Deliveryman.Application.Features.Deliveryman.Queries.GetDeliveryman;
using Global.Motorcycle.Application.Features.Motorycycles.Queries.GetDeliveryman;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.UnitTest.Application.Features.Motorcycles.Queries.GetById
{
    public class GetDeliverymanUnitTest
    {
        readonly Mock<IDeliverymanRepository> _deliverymanRepository;
        readonly Mock<ILogger<GetDeliverymanHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetDeliverymanHandler _handler;
        public GetDeliverymanUnitTest()
        {
            _deliverymanRepository = new Mock<IDeliverymanRepository>();
            _logger = new Mock<ILogger<GetDeliverymanHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetDeliverymanMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _handler = new GetDeliverymanHandler(_deliverymanRepository.Object, _logger.Object, 
                _notificationsHandler.Object, mapper);
        }

        [Fact]
        public async Task Deliveryman_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var deliverymenQuery = _fixture.Create<GetDeliverymanQuery>();
            var deliverymen = _fixture.Create<IEnumerable<DeliverymanEntity>>();

            _deliverymanRepository.Setup(x => x.GetAsync()).ReturnsAsync(deliverymen);

            var response = await _handler.Handle(deliverymenQuery, CancellationToken.None);

            Assert.NotNull(response);
            _deliverymanRepository.Verify(x => x.GetAsync(), Times.Once);
        }


        [Fact]
        public async Task Deliveryman_Should_Not_Be_Geted_When_Deliveryman_Not_Found()
        {
            var deliverymenQuery = _fixture.Create<GetDeliverymanQuery>();
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(deliverymenQuery, CancellationToken.None);

            Assert.Empty(response);
            _deliverymanRepository.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task Deliveryman_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var deliverymenQuery = _fixture.Create<GetDeliverymanQuery>();
            _deliverymanRepository.Setup(x => x.GetAsync()).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(deliverymenQuery, CancellationToken.None);

            Assert.Empty(response);
            _deliverymanRepository.Verify(x => x.GetAsync(), Times.Once);
        }
    }
}
