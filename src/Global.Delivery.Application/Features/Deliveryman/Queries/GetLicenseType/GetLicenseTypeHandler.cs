using AutoMapper;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.LicenseType.Application.Features.Deliveryman.Queries.GetLicenseType
{
    public class GetLicenseTypeHandler : IRequestHandler<GetLicenseTypeQuery, GetLicenseTypeResponse>
    {
        readonly IDeliverymanRepository _DeliveryRepository;
        readonly ILogger<GetLicenseTypeHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IMapper _mapper;

        public GetLicenseTypeHandler(IDeliverymanRepository DeliveryRepository, ILogger<GetLicenseTypeHandler> logger,
            INotificationsHandler notificationsHandler, IMapper mapper)
        {
            _DeliveryRepository = DeliveryRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _mapper = mapper;
        }

        public async Task<GetLicenseTypeResponse> Handle(GetLicenseTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryMan = await _DeliveryRepository.GetAsync(request.Id);

                if (deliveryMan == null)
                    return _notificationsHandler
                        .AddNotification("Deliveryman not found", ENotificationType.NotFound)
                        .ReturnDefault<GetLicenseTypeResponse>();

                var response = _mapper.Map<GetLicenseTypeResponse>(deliveryMan);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the LicenseType: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the LicenseType", ENotificationType.InternalError)
                        .ReturnDefault<GetLicenseTypeResponse>();
            }
        }
    }
}
