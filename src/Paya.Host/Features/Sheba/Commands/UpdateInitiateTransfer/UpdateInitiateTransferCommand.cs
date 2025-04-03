using MediatR;
using Paya.Host.Domain.Transfer.Enums;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.UpdateInitiateTransfer
{
    public class UpdateInitiateTransferCommand : IRequest<SuccessResponse>
    {
        public int RequestId { get; set; }
        public TransferStatus Status { get; set; }
        public string Note { get; set; }
    }

    public enum TransferStatus
    {
        Confirmed = 1,
        Canceled = 2,
    }
}
