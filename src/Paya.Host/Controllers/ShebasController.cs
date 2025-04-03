using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paya.Host.Features.Sheba.Commands.InitiateTransfer;
using Paya.Host.Features.Sheba.Commands.UpdateInitiateTransfer;
using Paya.Host.Features.Sheba.Queries.GetTransferRequestList;

namespace Paya.Host.Controllers
{
    [ApiController]
    public class ShebasController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ShebasController(IMediator mediator) => _mediator = mediator;

        [HttpPost("api/sheba")]
        public async Task<IActionResult> InitiateTransfer([FromBody] InitiateTransferCommand command)
        {
            await _mediator.Send(command);

            return Created();
        }

        [HttpGet("api/sheba")]
        public async Task<IActionResult> GetTransferRequestList()
        {
            var query = new GetTransferRequestListQuery();

            var basket = await _mediator.Send(query);

            return Ok(basket);
        }

        [HttpPut("api/sheba/{request-id}")]
        public async Task<IActionResult> UpdateInitiateTransfer([FromBody] UpdateInitiateTransferCommand command, int requestId)
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
