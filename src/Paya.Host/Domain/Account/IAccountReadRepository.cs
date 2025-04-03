using Paya.Host.Domain.Account.Entities;

namespace Paya.Host.Domain.Account
{
    public interface IAccountReadRepository
    {
        Task<UserAccount> GetUserAccount(int UserId);

    }
}
