using Paya.Host.Dtos;

namespace Paya.Host.Services.Contracts
{
    public interface IAccountService
    {
        Task ReserveAmount(int userId, decimal amount);
    }
}
