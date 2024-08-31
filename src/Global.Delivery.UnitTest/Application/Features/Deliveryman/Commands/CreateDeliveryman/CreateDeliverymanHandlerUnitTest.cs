using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.Delivery.Domain.Models.Notifications;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Contracts.Events;
using Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Data;
using AutoMapper;
using Global.Delivery.Domain.Models.Events.Deliveryman;
using Global.Delivery.Domain.Entities;

namespace Global.Delivery.UnitTest.Application.Features.Deliveryman.Commands.CreateDeliveryman
{
    public class CreateDeliverymanHandlerUnitTest
    {
        readonly Mock<IDeliverymanRepository> _deliveryRepository;
        readonly Mock<IDeliverymanProducer> _deliveryProducer;
        readonly Mock<ILogger<CreateDeliverymanHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly CreateDeliverymanHandler _handler;

        public CreateDeliverymanHandlerUnitTest()
        {
            _deliveryRepository = new Mock<IDeliverymanRepository>();
            _deliveryProducer = new Mock<IDeliverymanProducer>();
            _logger = new Mock<ILogger<CreateDeliverymanHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CreateDeliverymanMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new CreateDeliverymanHandler(_deliveryRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object, _unitOfWork.Object, _deliveryProducer.Object);
        }

        [Fact]
        public async Task Deliveryman_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var DeliveryCommand = _fixture.Create<CreateDeliverymanCommand>();

            _deliveryRepository.Setup(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);
            _deliveryRepository.Setup(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);

            var response = await _handler.Handle(DeliveryCommand, CancellationToken.None);

            Assert.NotNull(response);

            _deliveryRepository.Verify(x => x.AddAsync(It.IsAny<DeliverymanEntity>()), Times.Once);
            _deliveryProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedDeliverymanEvent>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _deliveryRepository.Verify(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
            _deliveryRepository.Verify(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Deliveryman_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var DeliveryCommand = _fixture.Create<CreateDeliverymanCommand>();

            _deliveryRepository.Setup(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);
            _deliveryRepository.Setup(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);

            _deliveryRepository.Setup(x => x.AddAsync(It.IsAny<DeliverymanEntity>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(DeliveryCommand, CancellationToken.None);

            Assert.Null(response);

            _deliveryRepository.Verify(x => x.AddAsync(It.IsAny<DeliverymanEntity>()), Times.Once);
            _deliveryProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedDeliverymanEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _deliveryRepository.Verify(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
            _deliveryRepository.Verify(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Deliveryman_Should_Not_Be_Created_When_Document_Already_Exists()
        {
            var DeliveryCommand = _fixture.Create<CreateDeliverymanCommand>();

            _deliveryRepository.Setup(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(true);

            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(DeliveryCommand, CancellationToken.None);

            Assert.Null(response);

            _deliveryRepository.Verify(x => x.AddAsync(It.IsAny<DeliverymanEntity>()), Times.Never);
            _deliveryProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedDeliverymanEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _deliveryRepository.Verify(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
            _deliveryRepository.Verify(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Deliveryman_Should_Not_Be_Created_When_LicenseNumber_Already_Exists()
        {
            var DeliveryCommand = _fixture.Create<CreateDeliverymanCommand>();

            _deliveryRepository.Setup(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);
            _deliveryRepository.Setup(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(true);


            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(DeliveryCommand, CancellationToken.None);

            Assert.Null(response);

            _deliveryRepository.Verify(x => x.AddAsync(It.IsAny<DeliverymanEntity>()), Times.Never);
            _deliveryProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedDeliverymanEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _deliveryRepository.Verify(x => x.DocumentExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Never);
            _deliveryRepository.Verify(x => x.LicenseNumberExistsAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}
