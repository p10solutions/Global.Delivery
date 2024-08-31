using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.Delivery.Domain.Models.Notifications;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Contracts.Events;
using Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Data;
using AutoMapper;
using Global.Delivery.Domain.Models.Events.Deliveryman;
using Global.Delivery.Domain.Entities;
using Global.Delivery.Domain.Contracts.Storage;

namespace Global.Delivery.UnitTest.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanHandlerUnitTest
    {
        readonly Mock<IDeliverymanRepository> _deliveryRepository;
        readonly Mock<IDeliverymanProducer> _deliveryProducer;
        readonly Mock<ILogger<UpdateDeliverymanHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly UpdateDeliverymanHandler _handler;
        readonly Mock<IDeliverymanStorage> _deliveryStorage;

        public UpdateDeliverymanHandlerUnitTest()
        {
            _deliveryRepository = new Mock<IDeliverymanRepository>();
            _deliveryProducer = new Mock<IDeliverymanProducer>();
            _logger = new Mock<ILogger<UpdateDeliverymanHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _deliveryStorage = new Mock<IDeliverymanStorage>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UpdateDeliverymanMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new UpdateDeliverymanHandler(_deliveryRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object, _unitOfWork.Object, _deliveryProducer.Object, _deliveryStorage.Object);
        }

        [Fact]
        public async Task Delivery_Should_Be_Updated_Successfully_When_All_Information_Has_Been_Submitted()
        {
            Guid id = Guid.NewGuid();
            var deliveryman = _fixture.Create<DeliverymanEntity>();
            var deliveryCommand = new UpdateDeliverymanCommand(
                id,
                "teste.png",
                MemoryStreamTest.GetPng()
            );

            _deliveryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(deliveryman);

            var response = await _handler.Handle(deliveryCommand, CancellationToken.None);

            Assert.NotNull(response);

            _deliveryStorage.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<string>()), Times.Once);
            _deliveryRepository.Verify(x => x.Update(It.IsAny<DeliverymanEntity>()), Times.Once);
            _deliveryProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedDeliverymanEvent>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _deliveryRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delivery_Should_Not_Be_Updated_When_An_Exception_Was_Thrown()
        {
            Guid id = Guid.NewGuid();
            var deliveryman = _fixture.Create<DeliverymanEntity>();
            var deliveryCommand = new UpdateDeliverymanCommand(
                id,
                "teste.png",
                MemoryStreamTest.GetPng()
            );

            _deliveryRepository.Setup(x => x.Update(It.IsAny<DeliverymanEntity>())).Throws(new Exception());
            _deliveryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(deliveryman);

            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(deliveryCommand, CancellationToken.None);

            Assert.Null(response);

            _deliveryRepository.Verify(x => x.Update(It.IsAny<DeliverymanEntity>()), Times.Once);
            _deliveryProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedDeliverymanEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _deliveryRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
            _deliveryStorage.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Delivery_Should_Not_Be_Updated_When_Not_Found()
        {
            Guid id = Guid.NewGuid();
            var deliveryCommand = new UpdateDeliverymanCommand(
                id,
                "teste.png",
                MemoryStreamTest.GetPng()
            );

            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(deliveryCommand, CancellationToken.None);

            Assert.Null(response);

            _deliveryStorage.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<string>()), Times.Never);
            _deliveryRepository.Verify(x => x.Update(It.IsAny<DeliverymanEntity>()), Times.Never);
            _deliveryProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedDeliverymanEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _deliveryRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
