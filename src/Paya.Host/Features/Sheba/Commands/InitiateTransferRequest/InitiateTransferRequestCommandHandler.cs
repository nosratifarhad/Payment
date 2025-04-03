using MediatR;
using Paya.Host.Domain.Transfer;
using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Domain.Transfer.Enums;
using Paya.Host.Dtos;
using Paya.Host.Services.Contracts;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.InitiateTransferRequest
{
    public class InitiateTransferRequestCommandHandler :
        IRequestHandler<InitiateTransferRequestCommand, SuccessResponse<TransferRequestDto>>
    {
        private readonly ITransactionWriteRepository _transactionWriteRepository;
        private readonly ITransferWriteRepository _transferWriteRepository;

        private readonly IAccountService _accountService;

        public InitiateTransferRequestCommandHandler(
            ITransactionWriteRepository transactionWriteRepository,
            ITransferWriteRepository transferWriteRepository,
            IAccountService accountService)
        {
            _transactionWriteRepository = transactionWriteRepository;
            _transferWriteRepository = transferWriteRepository;
            _accountService = accountService;
        }

        public async Task<SuccessResponse<TransferRequestDto>> Handle(
            InitiateTransferRequestCommand request, CancellationToken cancellationToken)
        {
            //open database transaction

            var reserveAmountDto = CreateReserveAmountDto(request);

            await _accountService.ReserveAmount(reserveAmountDto);

            var transferRequest = CreateTransferRequest(request);

            transferRequest.Id = await InitiateTransferRequest(transferRequest);

            await CreateTransaction(request, transferRequest.Id);

            //close database transaction

            var response = CreateSuccessResponse(transferRequest);

            return response;
        }

        private async Task<int> InitiateTransferRequest(TransferRequest transferRequest)
        {
            var transferRequestId =
                await _transferWriteRepository.CreateTransferRequest(transferRequest);

            return transferRequestId;
        }

        private async Task CreateTransaction(InitiateTransferRequestCommand request, int transferRequestId)
        {
            var transaction = new Transaction()
            {
                TransferRequestId = transferRequestId,
                Price = request.Price,
                TransactionType = TransactionType.Debit,
                CreatedAt = DateTime.Now
            };

            await _transactionWriteRepository.CreateTransaction(transaction);
        }

        private SuccessResponse<TransferRequestDto> CreateSuccessResponse(
            TransferRequest transferRequest)
        {
            var transferRequestDto = new TransferRequestDto()
            {
                Id = transferRequest.Id,
                Price = transferRequest.Price,
                Status = transferRequest.TransferStatus.ToString(),
                FromShebaNumber = transferRequest.FromShebaNumber,
                ToShebaNumber = transferRequest.ToShebaNumber,
                CreatedAt = transferRequest.CreatedAt
            };

            var successResponse = new SuccessResponse<TransferRequestDto>()
            {
                Message = "Request is saved successfully and is in pending status",
                request = transferRequestDto
            };

            return successResponse;
        }

        private ReserveAmountDto CreateReserveAmountDto(InitiateTransferRequestCommand request)
        {
            var reserveAmountDto =
                new ReserveAmountDto(request.UserId, request.Price, request.FromShebaNumber);

            return reserveAmountDto;
        }

        private TransferRequest CreateTransferRequest(InitiateTransferRequestCommand request)
        {
            var transferRequest = new TransferRequest()
            {
                UserId = request.UserId,
                FromShebaNumber = request.FromShebaNumber,
                ToShebaNumber = request.ToShebaNumber,
                Price = request.Price,
                TransferStatus = TransferRequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            return transferRequest;
        }
    }
}
