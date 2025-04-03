using Paya.Host.Dtos;

namespace Paya.Host.Domain.Transfer
{
    public interface ITransferReadRepository
    {
        Task<IEnumerable<TransferRequestDto>> GetTransferRequests();

        Task<TransferRequestDto> GetTransferRequest(int RequestId);
    }
}
