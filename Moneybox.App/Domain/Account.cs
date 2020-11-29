using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal WithdrawLimit = 0m;
        public const decimal PayInLimit = 4000m;
        public const decimal LowFundsLimit = 500m;
        public const decimal PayInNotificationLimit = 500m;
        public Guid Id { get; set; }
        public User User { get; set; }
        public decimal Balance { get; set; }
        public decimal Withdrawn { get; set; }
        public decimal PaidIn { get; set; }
        public bool IsWithdrawPossible(decimal withdrawAmount) => Balance - withdrawAmount >= WithdrawLimit;
        public bool IsPayInPossible(decimal payInAmount) => PaidIn + payInAmount <= PayInLimit;
        public bool IsLowFunds => Balance < LowFundsLimit;
        public bool IsNearlyAtPayLimit => PayInLimit - PaidIn < PayInNotificationLimit;
        public void Withdraw(decimal withdrawAmount)
        {
            if (!IsWithdrawPossible(withdrawAmount))
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            Balance -= withdrawAmount;
            Withdrawn -= withdrawAmount;
        }
        public void PayIn(decimal payInAmount)
        {
            if (!IsPayInPossible(payInAmount))
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            Balance += payInAmount;
            PaidIn += payInAmount;
        }
    }
}
