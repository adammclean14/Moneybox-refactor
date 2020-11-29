using Moneybox.App.Domain.Services;
using Moneybox.App.DataAccess;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository _accountRepository;
        private INotificationService _notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {            
            var fromAccount = _accountRepository.GetAccountById(fromAccountId);
            fromAccount.Withdraw(amount);
            _accountRepository.Update(fromAccount);
            NotifyEmailIfFundsLow(fromAccount);
        }

        private void NotifyEmailIfFundsLow(Account fromAccount)
        {
            if (fromAccount.IsLowFunds)
            {
                _notificationService.NotifyFundsLow(fromAccount.User.Email);
            }
        }
    }
}
