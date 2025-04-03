using Paya.Host.Domain.Account;
using Paya.Host.Services.Contracts;

namespace Paya.Host.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountWriteRepository _accountWriteRepository;
        private readonly IAccountReadRepository _accountReadRepository;

        public AccountService(IAccountWriteRepository accountWriteRepository,
            IAccountReadRepository accountReadRepository)
        {
            _accountWriteRepository = accountWriteRepository;
            _accountReadRepository = accountReadRepository;
        }

        public async Task ReserveAmount(int userId, decimal price)
        {
            var userAccount = await _accountReadRepository.GetUserAccount(userId);
            if (userAccount == null)
                throw new Exception("یافت نشد");

            if (userAccount.Balance < price)
                throw new Exception("موجودی کافی برای انجام انتقال وجه وجود ندارد.");

            userAccount.Balance -= price;
            userAccount.UpdateAt = DateTime.Now;

            await _accountWriteRepository.UpdateUserAccount(userAccount);
        }
    }
}
