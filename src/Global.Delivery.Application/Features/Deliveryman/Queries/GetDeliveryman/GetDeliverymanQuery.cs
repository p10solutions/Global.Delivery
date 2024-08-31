using Global.Delivery.Application.Features.Common;
using Global.Motorcycle.Application.Features.Motorycycles.Queries.GetDeliveryman;
using MediatR;

namespace Global.Delivery.Application.Features.Deliveryman.Queries.GetDeliveryman
{
    public class GetDeliverymanQuery : CommandBase<GetDeliverymanQuery>, IRequest<IEnumerable<GetDeliverymanResponse>>
    {
        public GetDeliverymanQuery() : base(new GetDeliverymanQueryValidator())
        {
        }
    }
}
