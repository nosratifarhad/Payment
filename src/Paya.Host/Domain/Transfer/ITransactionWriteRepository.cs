using Paya.Host.Domain.Transfer.Entities;

namespace Paya.Host.Domain.Transfer
{
    public interface ITransactionWriteRepository
    {
        Task CreateTransferRequest(Transaction transaction);
    }
}
