using Global.Delivery.Application.Features.Common;
using Global.Motorcycle.Application.Features.Motorycycles.Queries.GetLicenseType;
using MediatR;

namespace Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType
{
    public class GetLicenseTypeQuery : CommandBase<GetLicenseTypeQuery>, IRequest<GetLicenseTypeResponse>
    {
        public GetLicenseTypeQuery(Guid id) : base(new GetLicenseTypeQueryValidator())
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
