using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paya.Host.Features.Sheba.Commands.InitiateTransferRequest;
using Paya.Host.Features.Sheba.Commands.UpdateInitiateTransferRequest;
using Paya.Host.Features.Sheba.Queries.GetTransferRequests;

namespace Paya.Host.Controllers
{
    [ApiController]
    public class ShebasController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ShebasController(IMediator mediator) => _mediator = mediator;

        [HttpPost("api/sheba")]
        public async Task<IActionResult> InitiateTransferRequest([FromBody] InitiateTransferRequestCommand command)
        {
            command.UserId = 1; //Get from token

            var response = await _mediator.Send(command);

            return Created("", response);
        }

        [HttpGet("api/sheba")]
        public async Task<IActionResult> GetTransferRequests()
        {
            var query = new GetTransferRequestsQuery();

            var transferRequestCollectionDto = await _mediator.Send(query);

            return Ok(transferRequestCollectionDto);
        }

        [HttpPut("api/sheba/{request-id}")]
        public async Task<IActionResult> UpdateInitiateTransfer([FromBody] UpdateInitiateTransferRequestCommand command, int requestId)
        {
            if (requestId == 0)
                return BadRequest();

            if (requestId != command.RequestId)
                return BadRequest();

            await _mediator.Send(command);

            return Created();
        }
    }
}
