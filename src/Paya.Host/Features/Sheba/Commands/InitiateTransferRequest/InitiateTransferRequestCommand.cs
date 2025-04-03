using MediatR;
using Paya.Host.Dtos;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.InitiateTransferRequest
{
    public record InitiateTransferRequestCommand(
      decimal Price, string FromShebaNumber, string ToShebaNumber, string Note)
        : IRequest<SuccessResponse<TransferRequestDto>>
    {
        public int UserId { get; set; }
    }
}
