using Paya.Host.Domain.Account.Entities;

namespace Paya.Host.Domain.Account
{
    public interface IAccountWriteRepository
    {
        Task UpdateUserAccount(UserAccount userAccount);
    }
}
