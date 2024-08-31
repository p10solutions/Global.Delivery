using Global.Delivery.Api.Controllers.Base;
using Global.Delivery.Application.Features.Deliveryman.Commands.CreateDeliveryman;
using Global.Delivery.Application.Features.Deliveryman.Commands.UpdateDeliveryman;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetDeliveryman;
using Global.Delivery.Application.Features.Deliveryman.Queries.GetLicenseType;
using Global.Delivery.Domain.Contracts.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Global.Delivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverymanController(IMediator mediator, INotificationsHandler notifications) : ApiControllerBase(mediator, notifications)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateDeliverymanResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(CreateDeliverymanCommand CreateDeliverymanCommand)
            => await SendAsync(CreateDeliverymanCommand, HttpStatusCode.Created);

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateDeliverymanResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchAsync(Guid id, IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            return await SendAsync(new UpdateDeliverymanCommand(id, file.FileName, memoryStream)); 
        }

        [HttpGet("{id}/licensetype")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLicenseTypeResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLicenseType(Guid id)
            => await SendAsync(new GetLicenseTypeQuery(id));

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetDeliverymanResponse>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
            => await SendAsync(new GetDeliverymanQuery());
    }
}
