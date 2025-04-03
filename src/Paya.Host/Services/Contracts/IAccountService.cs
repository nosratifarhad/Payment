using Paya.Host.Dtos;

namespace Paya.Host.Services.Contracts
{
    public interface IAccountService
    {
        Task ReserveAmount(ReserveAmountDto reserveAmountDto);

        Task UpdateReserveAmount(UpdateReserveAmountDto updateReserveAmountDto);
    }
}
