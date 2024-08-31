using Global.Delivery.Application.Features.Common;
using MediatR;

namespace Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman
{
    public class UpdateDeliverymanCommand(Guid id, string licenseImage, MemoryStream file)
        : CommandBase<UpdateDeliverymanCommand>(new UpdateDeliverymanCommandValidator()), IRequest<UpdateDeliverymanResponse>
    {
        public Guid Id { get; init; } = id;
        public string LicenseImage { get; set; } = licenseImage;
        public MemoryStream File { get; set; } = file;
    }
}
