using AutoMapper;
using Global.Delivery.Domain.Contracts.Data;
using Global.Delivery.Domain.Contracts.Data.Repositories;
using Global.Delivery.Domain.Contracts.Events;
using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Entities;
using Global.Delivery.Domain.Models.Events.Deliveryman;
using Global.Delivery.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman
{
    public class CreateDeliverymanHandler : IRequestHandler<CreateDeliverymanCommand, CreateDeliverymanResponse>
    {
        readonly IDeliverymanRepository _deliveryRepository;
        readonly ILogger<CreateDeliverymanHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly IDeliverymanProducer _deliveryProducer;

        public CreateDeliverymanHandler(IDeliverymanRepository deliveryRepository, ILogger<CreateDeliverymanHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork, IDeliverymanProducer deliveryProducer)
        {
            _deliveryRepository = deliveryRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _deliveryProducer = deliveryProducer;
        }

        public async Task<CreateDeliverymanResponse> Handle(CreateDeliverymanCommand request, CancellationToken cancellationToken)
        {
            var deliveryMan = _mapper.Map<DeliverymanEntity>(request);

            try
            {
                if (await _deliveryRepository.LicenseNumberExistsAsync(deliveryMan.LicenseNumber, deliveryMan.Id))
                {
                    _logger.LogWarning("There is already a Deliveryman with that license number: {LicenseNumber}", request.LicenseNumber);

                    return _notificationsHandler
                        .AddNotification("There is already a Deliveryman with that license number", ENotificationType.BusinessValidation)
                        .ReturnDefault<CreateDeliverymanResponse>();
                }

                if (await _deliveryRepository.DocumentExistsAsync(deliveryMan.Document, deliveryMan.Id))
                {
                    _logger.LogWarning("There is already a Deliveryman with that document: {Document}", request.LicenseNumber);

                    return _notificationsHandler
                        .AddNotification("There is already a Deliveryman with that document", ENotificationType.BusinessValidation)
                        .ReturnDefault<CreateDeliverymanResponse>();
                }

                await _deliveryRepository.AddAsync(deliveryMan);
                await _unitOfWork.CommitAsync();

                var @event = _mapper.Map<CreatedDeliverymanEvent>(deliveryMan);

                await _deliveryProducer.SendCreatedEventAsync(@event);

                var response = _mapper.Map<CreateDeliverymanResponse>(deliveryMan);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to insert the Deliveryman: {Exception}", ex.Message);

               return _notificationsHandler
                    .AddNotification("An error occurred when trying to insert the Deliveryman", ENotificationType.InternalError)
                    .ReturnDefault<CreateDeliverymanResponse>();
            }
        }
    }
}
