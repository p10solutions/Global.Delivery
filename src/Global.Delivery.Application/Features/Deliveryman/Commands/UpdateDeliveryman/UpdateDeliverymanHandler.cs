using AutoMapper;
using Global.Delivery.Domain.Contracts.Data;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Events;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Contracts.Storage;
using Global.Delivery.Domain.Models.Events.Deliveryman;
using Global.Delivery.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanHandler : IRequestHandler<UpdateDeliverymanCommand, UpdateDeliverymanResponse>
    {
        readonly IDeliverymanRepository _deliveryRepository;
        readonly ILogger<UpdateDeliverymanHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly IDeliverymanProducer _deliveryProducer;
        readonly IDeliverymanStorage _deliverymanStorage;

        public UpdateDeliverymanHandler(IDeliverymanRepository deliveryRepository, ILogger<UpdateDeliverymanHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork, IDeliverymanProducer deliveryProducer, IDeliverymanStorage deliverymanStorage)
        {
            _deliveryRepository = deliveryRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _deliveryProducer = deliveryProducer;
            _deliverymanStorage = deliverymanStorage;
        }

        public async Task<UpdateDeliverymanResponse> Handle(UpdateDeliverymanCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var deliveryman = await _deliveryRepository.GetAsync(request.Id);

                if (deliveryman is null)
                    return _notificationsHandler
                        .AddNotification("Deliveryman not found", ENotificationType.NotFound)
                        .ReturnDefault<UpdateDeliverymanResponse>();

                await _deliverymanStorage.UploadAsync(request.File, request.LicenseImage);

                deliveryman.LicenseImage = request.LicenseImage;

                _deliveryRepository.Update(deliveryman);
                await _unitOfWork.CommitAsync();

                var @event = _mapper.Map<UpdatedDeliverymanEvent>(deliveryman);

                await _deliveryProducer.SendUpdatedEventAsync(@event);

                var response = _mapper.Map<UpdateDeliverymanResponse>(deliveryman);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to update the Deliveryman: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to insert the Deliveryman", ENotificationType.InternalError)
                     .ReturnDefault<UpdateDeliverymanResponse>();
            }
        }
    }
}
