using FluentValidation;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetDeliveryman;

namespace Global.Motorcycle.Application.Features.Motorycycles.Queries.GetDeliveryman
{
    public class GetDeliverymanQueryValidator : AbstractValidator<GetDeliverymanQuery>
    {
        public GetDeliverymanQueryValidator()
        {
        }
    }
}
