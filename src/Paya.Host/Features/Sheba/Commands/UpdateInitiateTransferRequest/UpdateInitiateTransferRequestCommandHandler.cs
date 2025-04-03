using MediatR;
using Paya.Host.Domain.Transfer;
using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Domain.Transfer.Enums;
using Paya.Host.Dtos;
using Paya.Host.Exceptions;
using Paya.Host.Services.Contracts;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Features.Sheba.Commands.UpdateInitiateTransferRequest
{
    public class UpdateInitiateTransferRequestCommandHandler :
        IRequestHandler<UpdateInitiateTransferRequestCommand, SuccessResponse<TransferRequestDto>>
    {
        private readonly ITransferReadRepository _transferReadRepository;
        private readonly ITransferWriteRepository _transferWriteRepository;
        private readonly ITransactionWriteRepository _transactionWriteRepository;
        private readonly IAccountService _accountService;

        public UpdateInitiateTransferRequestCommandHandler(
            ITransferReadRepository transferReadRepository,
            ITransferWriteRepository transferWriteRepository,
             ITransactionWriteRepository transactionWriteRepository,
            IAccountService accountService)
        {
            _transferReadRepository = transferReadRepository;
            _transferWriteRepository = transferWriteRepository;
            _transactionWriteRepository = transactionWriteRepository;

            _accountService = accountService;
        }

        public async Task<SuccessResponse<TransferRequestDto>> Handle(UpdateInitiateTransferRequestCommand request, CancellationToken cancellationToken)
        {
            var transferRequestDto = await _transferReadRepository.GetTransferRequest(request.RequestId);
            if (transferRequestDto == null)
                throw new BusinessException("TransferRequest Not Found",
                    BusinessErrorCodes.TransferRequestNotFound.ToString());

            if (transferRequestDto.Status != TransferRequestStatus.Pending.ToString())
                throw new BusinessException("درخواست قبلاً پردازش شده", "InvalidStatusChange");

            await UpdateReserveAmount(request, transferRequestDto);

            await UpdateTransferRequest(request, transferRequestDto);

            await CreateTransaction(request, transferRequestDto);

            var successResponse = CreateSuccessResponse(transferRequestDto);

            return successResponse;
        }

        private async Task UpdateReserveAmount(
            UpdateInitiateTransferRequestCommand request,
            TransferRequestDto transferRequestDto)
        {
            var updateReserveAmountDto = new UpdateReserveAmountDto()
            {
                Id = transferRequestDto.Id,
                UserId = 555,//transferRequestDto.user
                Price = transferRequestDto.Price,
                Status = request.Status,
            };

            await _accountService.UpdateReserveAmount(updateReserveAmountDto);
        }

        private async Task UpdateTransferRequest(UpdateInitiateTransferRequestCommand request, TransferRequestDto transferRequestDto)
        {
            var transferRequest = new TransferRequest()
            {
                Id = transferRequestDto.Id,
                TransferStatus = request.Status == "Confirmed"
                    ? TransferRequestStatus.Confirmed
                    : TransferRequestStatus.Canceled,
                Note = request.Note
            };

            await _transferWriteRepository.UpdateTransferRequest(transferRequest);
        }

        private async Task CreateTransaction(UpdateInitiateTransferRequestCommand request, TransferRequestDto transferRequestDto)
        {
            if (request.Status == "Canceled")
            {
                var transaction = new Transaction()
                {
                    TransferRequestId = transferRequestDto.Id,
                    Price = transferRequestDto.Price,
                    TransactionType = TransactionType.Credit,
                    CreatedAt = DateTime.Now
                };

                await _transactionWriteRepository.CreateTransaction(transaction);
            }
        }

        private SuccessResponse<TransferRequestDto> CreateSuccessResponse(TransferRequestDto transferRequestDto)
        {
            var successTransferResponse = new TransferRequestDto()
            {
                Id = transferRequestDto.Id,
                Status = transferRequestDto.Status.ToString(),
                FromShebaNumber = transferRequestDto.FromShebaNumber,
                ToShebaNumber = transferRequestDto.ToShebaNumber,
                Price = transferRequestDto.Price,
                CreatedAt = DateTime.Now
            };

            var successResponse = new SuccessResponse<TransferRequestDto>()
            {
                Message = "Request is Confirmed!",
                request = successTransferResponse
            };

            return successResponse;
        }
    }
}
