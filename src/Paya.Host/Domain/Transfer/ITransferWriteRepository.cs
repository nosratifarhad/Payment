using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Dtos;

namespace Paya.Host.Domain.Transfer
{
    public interface ITransferWriteRepository
    {
        Task<int> CreateTransferRequest(TransferRequest transferRequest);
    }
}
