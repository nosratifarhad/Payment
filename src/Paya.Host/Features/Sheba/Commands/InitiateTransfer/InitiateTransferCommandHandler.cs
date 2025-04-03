using MediatR;
using Paya.Host.Domain.Transfer.Enums;
using Paya.Host.Dtos;
using Paya.Host.Services.Contracts;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.InitiateTransfer
{
    public class InitiateTransferCommandHandler : IRequestHandler<InitiateTransferCommand, SuccessResponse>
    {
        private readonly ITransferService _transferService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        public InitiateTransferCommandHandler(ITransferService transferService,
            IAccountService accountService,
            ITransactionService transactionService)
        {
            _transferService = transferService;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public async Task<SuccessResponse> Handle(InitiateTransferCommand request, CancellationToken cancellationToken)
        {
            //open database transaction

            await _accountService.ReserveAmount(request.UserId, request.Price);

            var transferRequestId = await InitiateTransfer(request);

            await CreateTransaction(request, transferRequestId);

            //close database transaction

            var successResponse = CreateSuccessResponse(request, transferRequestId);

            return successResponse;
        }

        private async Task<int> InitiateTransfer(InitiateTransferCommand request)
        {
            var initiateTransferDto = new TransferRequestDto()
            {
                UserId = request.UserId,
                Price = request.Price,
                FromShebaNumber = request.FromShebaNumber,
                ToShebaNumber = request.ToShebaNumber,
                TransferStatus = TransferRequestStatus.Reserved,
                Note = request.Note,
                CreatedAt = DateTime.UtcNow
            };

            var transferRequestId = await _transferService.InitiateTransfer(initiateTransferDto);

            return transferRequestId;
        }

        private async Task CreateTransaction(InitiateTransferCommand request, int transferRequestId)
        {
            var transactionDto = new TransactionDto()
            {
                TransferRequestId = transferRequestId,
                Price = request.Price,
                Description = request.Note,
                TransactionType = TransactionType.Debit
            };

            await _transactionService.CreateTransaction(transactionDto);
        }

        private SuccessResponse CreateSuccessResponse(InitiateTransferCommand request, int transferRequestId)
        {
            var successTransferResponse = new SuccessTransferResponse()
            {
                Id = transferRequestId,
                Price = request.Price,
                FromShebaNumber = request.FromShebaNumber,
                ToShebaNumber = request.ToShebaNumber,
                Status = "pending",
                CreatedAt = DateTime.Now
            };

            var successResponse = new SuccessResponse()
            {
                Message = "Request is saved successfully and is in pending status",
                request = successTransferResponse
            };

            return successResponse;
        }
    }
}
