using MediatR;
using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Domain.Transfer.Enums;
using Paya.Host.Dtos;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.UpdateInitiateTransfer
{
    public class ConfirmorCancelShebaRequestCommandHandler : IRequestHandler<UpdateInitiateTransferCommand, SuccessResponse>
    {

        public async Task<SuccessResponse> Handle(UpdateInitiateTransferCommand request, CancellationToken cancellationToken)
        {
            var transfer = new TransferRequest();//methos call Get TransferRequest By RequestId;
            if(transfer == null)
                throw new Exception("Error Message");

            if (request.Status == TransferStatus.Confirmed)
                transfer.TransferStatus = TransferRequestStatus.Confirmed;
            else
                if (request.Status == TransferStatus.Canceled)
                transfer.TransferStatus = TransferRequestStatus.Canceled;

            ///
            // method call for update TransferStatus
            ///

            var successResponse = CreateSuccessResponse(transfer);

            return successResponse;
        }

        private SuccessResponse CreateSuccessResponse(TransferRequest transfer)
        {
            var successTransferResponse = new SuccessTransferResponse()
            {
                Id = transfer.Id,
                Status = transfer.TransferStatus.ToString(),
                FromShebaNumber = transfer.FromShebaNumber,
                ToShebaNumber = transfer.ToShebaNumber,
                Price = transfer.Price,
                CreatedAt = DateTime.Now
            };

            var successResponse = new SuccessResponse()
            {
                Message = "Request is Confirmed!",
                request = successTransferResponse
            };

            return successResponse;
        }
    }
}
