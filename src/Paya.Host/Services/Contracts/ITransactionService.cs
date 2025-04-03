using Paya.Host.Dtos;

namespace Paya.Host.Services.Contracts
{
    public interface ITransactionService
    {
        Task CreateTransaction(TransactionDto transactionDto);
    }
}
