using Paya.Host.Domain.Account;
using Paya.Host.Domain.Transfer.Enums;
using Paya.Host.Dtos;
using Paya.Host.Exceptions;
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

        public async Task ReserveAmount(ReserveAmountDto reserveAmountDto)
        {
            var userAccount = await _accountReadRepository.GetUserAccount(reserveAmountDto.UserId);
            if (userAccount == null)
                throw new BusinessException("یافت نشد", ((int)BusinessErrorCodes.UserAccountNotFound).ToString());

            if (userAccount.Balance < reserveAmountDto.Price)
                throw new BusinessException("موجودی کافی برای انجام انتقال وجه وجود ندارد.",
                    ((int)BusinessErrorCodes.NotEnoughInventory).ToString());

            userAccount.ReservedAmount += reserveAmountDto.Price;
            userAccount.UpdateAt = DateTime.Now;

            await _accountWriteRepository.UpdateUserAccount(userAccount);
        }

        public async Task UpdateReserveAmount(UpdateReserveAmountDto updateReserveAmountDto)
        {
            var amount = await _accountReadRepository.GetUserAccount(updateReserveAmountDto.UserId);
            if (amount == null)
                throw new BusinessException("Amount Not Found",
                    ((int)BusinessErrorCodes.AmountNotFound).ToString());

            if (updateReserveAmountDto.Status == "Confirmed")
            {
                amount.ReservedAmount = 0;
            }
            if (updateReserveAmountDto.Status == "Canceled")
            {
                amount.Balance += amount.ReservedAmount;
                amount.ReservedAmount = 0;
            }

            amount.UpdateAt = DateTime.Now;

            await _accountWriteRepository.UpdateUserAccount(amount);
        }
    }
}
