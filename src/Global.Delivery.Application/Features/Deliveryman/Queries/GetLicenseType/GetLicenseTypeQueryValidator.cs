using FluentValidation;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType;

namespace Global.Motorcycle.Application.Features.Motorycycles.Queries.GetLicenseType
{
    public class GetLicenseTypeQueryValidator : AbstractValidator<GetLicenseTypeQuery>
    {
        public GetLicenseTypeQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
