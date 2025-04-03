using MediatR;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.InitiateTransfer
{
    public record InitiateTransferCommand(
       int UserId, decimal Price, string FromShebaNumber, string ToShebaNumber, string Note) : IRequest<SuccessResponse>
    {
    }
}
