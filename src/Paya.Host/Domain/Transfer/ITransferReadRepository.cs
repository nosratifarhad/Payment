using Paya.Host.Dtos;

namespace Paya.Host.Domain.Transfer
{
    public interface ITransferReadRepository
    {
        Task<IEnumerable<GetTransferRequestDto>> GetTransferRequests();
    }
}
