using Paya.Host.Domain.Transfer;
using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Dtos;
using Paya.Host.Services.Contracts;

namespace Paya.Host.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionWriteRepository _transactionWriteRepository;

        public TransactionService(ITransactionWriteRepository transactionWriteRepository)
        {
            _transactionWriteRepository = transactionWriteRepository;
        }

        public async Task CreateTransaction(TransactionDto transactionDto)
        {
            var transaction = new Transaction()
            {
                TransferRequestId = transactionDto.TransferRequestId,
                Price = transactionDto.Price,
                Description = transactionDto.Description,
                TransactionType = transactionDto.TransactionType,
                CreatedAt = DateTime.Now
            };

            await _transactionWriteRepository.CreateTransferRequest(transaction);
        }
    }
}
