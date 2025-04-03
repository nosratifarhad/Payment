using Paya.Host.Domain.Transfer.Entities;

namespace Paya.Host.Domain.Transfer
{
    public interface ITransferWriteRepository
    {
        Task<int> CreateTransferRequest(TransferRequest transferRequest);

        Task UpdateTransferRequest(TransferRequest transferRequest);
    }
}
