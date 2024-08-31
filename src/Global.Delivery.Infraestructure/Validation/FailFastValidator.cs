using Global.Delivery.Domain.Contracts.Notifications;
using Global.Delivery.Domain.Contracts.Validation;
using Global.Delivery.Domain.Models.Notifications;
using MediatR;

namespace Global.Delivery.Infraestructure.Validation
{
    public class FailFastValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidableEntity
    {
        readonly INotificationsHandler _notificationHandler;

        public FailFastValidator(INotificationsHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!request.Validate())
                return _notificationHandler
                    .AddNotification(request.Errors, ENotificationType.BusinessValidation)
                    .ReturnDefault<TResponse>();

            return await next();
        }
    }
}
