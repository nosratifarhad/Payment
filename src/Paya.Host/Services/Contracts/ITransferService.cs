using Paya.Host.Dtos;

namespace Paya.Host.Services.Contracts
{
    public interface ITransferService
    {
        Task<int> InitiateTransfer(TransferRequestDto initiateTransferDto);
    }
}
