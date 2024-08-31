using AutoFixture;
using AutoMapper;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Entities;
using Global.Delivery.Domain.Models.Notifications;
using Global.LicenseType.Application.Features.Deliveryman.Queries.GetLicenseType;
using Global.Motorcycle.Application.Features.Motorycycles.Queries.GetLicenseType;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.Motorcycle.UnitTest.Application.Features.Motorcycles.Queries.GetById
{
    public class GetLicenseTypeUnitTest
    {
        readonly Mock<IDeliverymanRepository> _deliverymanRepository;
        readonly Mock<ILogger<GetLicenseTypeHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetLicenseTypeHandler _handler;
        public GetLicenseTypeUnitTest()
        {
            _deliverymanRepository = new Mock<IDeliverymanRepository>();
            _logger = new Mock<ILogger<GetLicenseTypeHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetLicenseTypeMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _handler = new GetLicenseTypeHandler(_deliverymanRepository.Object, _logger.Object, 
                _notificationsHandler.Object, mapper);
        }

        [Fact]
        public async Task LicenseType_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var licesenTypeQuery = _fixture.Create<GetLicenseTypeQuery>();
            var licenseType = _fixture.Create<DeliverymanEntity>();

            _deliverymanRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(licenseType);

            var response = await _handler.Handle(licesenTypeQuery, CancellationToken.None);

            Assert.NotNull(response);
            _deliverymanRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async Task LicenseType_Should_Not_Be_Geted_When_LicenseType_Not_Found()
        {
            var licesenTypeQuery = _fixture.Create<GetLicenseTypeQuery>();
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(licesenTypeQuery, CancellationToken.None);

            Assert.Null(response);
            _deliverymanRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task LicenseType_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var licesenTypeQuery = _fixture.Create<GetLicenseTypeQuery>();
            _deliverymanRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(licesenTypeQuery, CancellationToken.None);

            Assert.Null(response);
            _deliverymanRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
