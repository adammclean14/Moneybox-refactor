using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository _accountRepository;
        private INotificationService _notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        //Refactored method to meet single responsibility principle as much as possible
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccountById(fromAccountId);
            var toAccount = _accountRepository.GetAccountById(toAccountId);

            fromAccount.Withdraw(amount);
            toAccount.PayIn(amount);

            UpdateAccounts(fromAccount, toAccount);
            NotifyCustomerIfNecessary(fromAccount, toAccount);
        }

        private void UpdateAccounts(Account fromAccount, Account toAccount)
        {
            _accountRepository.Update(fromAccount);
            _accountRepository.Update(toAccount);
        }

        private void NotifyCustomerIfNecessary(Account fromAccount, Account toAccount)
        {
            NotifyIfLowFunds(fromAccount);
            NotifyIfApproachingPayInLimit(toAccount);
        }

        private void NotifyIfLowFunds(Account fromAccount)
        {
            if (fromAccount.IsLowFunds)
            {
                _notificationService.NotifyFundsLow(fromAccount.User.Email);
            }
        }

        private void NotifyIfApproachingPayInLimit(Account toAccount)
        {
            if (toAccount.IsNearlyAtPayLimit)
            {
                _notificationService.NotifyApproachingPayInLimit(toAccount.User.Email);
            }
        }
    }
}
