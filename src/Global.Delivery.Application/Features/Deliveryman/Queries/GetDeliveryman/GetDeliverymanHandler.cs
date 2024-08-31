using AutoMapper;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetDeliveryman;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Deliveryman.Application.Features.Deliveryman.Queries.GetDeliveryman
{
    public class GetDeliverymanHandler : IRequestHandler<GetDeliverymanQuery, IEnumerable<GetDeliverymanResponse>>
    {
        readonly IDeliverymanRepository _deliveryRepository;
        readonly ILogger<GetDeliverymanHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IMapper _mapper;

        public GetDeliverymanHandler(IDeliverymanRepository DeliveryRepository, ILogger<GetDeliverymanHandler> logger,
            INotificationsHandler notificationsHandler, IMapper mapper)
        {
            _deliveryRepository = DeliveryRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDeliverymanResponse>> Handle(GetDeliverymanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryMen = await _deliveryRepository.GetAsync();

                var response = _mapper.Map<IEnumerable<GetDeliverymanResponse>>(deliveryMen);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the Deliverymen: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the Deliverymen", ENotificationType.InternalError)
                        .ReturnDefault<IEnumerable<GetDeliverymanResponse>>();
            }
        }
    }
}
