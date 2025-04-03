using Paya.Host.Domain.Transfer;
using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Dtos;
using Paya.Host.Services.Contracts;

namespace Paya.Host.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferWriteRepository _transferWriteRepository;

        public TransferService(ITransferWriteRepository transferWriteRepository)
        {
            _transferWriteRepository = transferWriteRepository;
        }

        public async Task<int> InitiateTransfer(TransferRequestDto transferRequestDto)
        {
            var transferRequest = new TransferRequest()
            {
                UserId = transferRequestDto.UserId,
                FromShebaNumber = transferRequestDto.FromShebaNumber,
                ToShebaNumber = transferRequestDto.ToShebaNumber,
                Price = transferRequestDto.Price,
                TransferStatus = transferRequestDto.TransferStatus,
                CreatedAt = transferRequestDto.CreatedAt
            };

            var transferRequestId = await _transferWriteRepository.CreateTransferRequest(transferRequest);

            return transferRequestId;
        }
    }
}
