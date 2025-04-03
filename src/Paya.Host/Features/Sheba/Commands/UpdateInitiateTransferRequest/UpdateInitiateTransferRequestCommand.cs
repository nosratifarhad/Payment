using MediatR;
using Paya.Host.Dtos;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.UpdateInitiateTransferRequest
{
    public class UpdateInitiateTransferRequestCommand : IRequest<SuccessResponse<TransferRequestDto>>
    {
        public int RequestId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
