using Moneybox.App;

using NUnit.Framework;
using System;

namespace MoneyBox.App.Testing
{
    [TestFixture]
    public class AccountTests
    {
        //This class tests the class Account for all boolean properties as well as the exceptions in the Withdraw and PayIn methods.
        //It should also check that withdrawel and deposit calculations work too.
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account();
        }

        [TestCase(100, 0)]
        [TestCase(100, 50)]
        [TestCase(100, 100)]
        public void Should_Be_Able_To_Withdraw(decimal balance, decimal withdrawelAmount)
        {
            //Arrange
            _account.Balance = balance;

            //Act
            var isWithdrawPossible = _account.IsWithdrawPossible(withdrawelAmount);

            //Assert
            Assert.That(isWithdrawPossible, Is.True);
        }

        [TestCase(100, 101)]
        [TestCase(100, 200)]
        [TestCase(100, 500)]
        public void Should_Not_Be_Able_To_Withdraw(decimal balance, decimal withdrawelAmount)
        {
            //Arrange
            _account.Balance = balance;

            //Act
            var isWithdrawPossible = _account.IsWithdrawPossible(withdrawelAmount);

            //Assert
            Assert.That(isWithdrawPossible, Is.False);
        }

        [TestCase(3500, 0)]
        [TestCase(3500, 250)]
        [TestCase(3500, 500)]
        public void Should_Pay_In_Be_Possible(decimal paidIn, decimal payInAmount)
        {
            //Arrange
            _account.PaidIn = paidIn;

            //Act
            var isPayInPossible = _account.IsPayInPossible(payInAmount);

            //Assert
            Assert.That(isPayInPossible, Is.True);
        }

        [TestCase(3500, 600)]
        [TestCase(3500, 1000)]
        [TestCase(3500, 2000)]
        public void Should_Pay_In_Not_Be_Possible(decimal paidIn, decimal payInAmount)
        {
            //Arrange
            _account.PaidIn = paidIn;

            //Act
            var isPayInPossible = _account.IsPayInPossible(payInAmount);

            //Assert
            Assert.That(isPayInPossible, Is.False);
        }

        [TestCase(0)]
        [TestCase(250)]
        [TestCase(499)]
        public void Should_Be_Low_Funds(decimal balance)
        {
            //Arrange
            _account.Balance = balance;

            //Act
            var isLowFunds = _account.IsLowFunds;

            //Assert
            Assert.That(isLowFunds, Is.True);
        }

        [TestCase(500)]
        [TestCase(501)]
        [TestCase(1000)]
        public void Should_Not_Be_Low_Funds(decimal balance)
        {
            //Arrange
            _account.Balance = balance;

            //Act
            var isLowFunds = _account.IsLowFunds;

            //Assert
            Assert.That(isLowFunds, Is.False);
        }

        [TestCase(3501)]
        [TestCase(3750)]
        [TestCase(3999)]

        public void Should_Notify_Nearly_At_Pay_In_Limit(decimal paidIn)
        {
            //Arrange
            _account.PaidIn = paidIn;

            //Act
            var canNotifyFundsLow = _account.IsNearlyAtPayLimit;

            //Assert
            Assert.That(canNotifyFundsLow, Is.True);
        }

        [TestCase(0)]
        [TestCase(1000)]
        [TestCase(3500)]
        public void Should_Not_Notify_Nearly_At_Pay_In_Limit(decimal paidIn)
        {
            //Arrange
            _account.PaidIn = paidIn;

            //Act
            var canNotifyFundsLow = _account.IsNearlyAtPayLimit;

            //Assert
            Assert.That(canNotifyFundsLow, Is.False);
        }

        [TestCase(100, 101)]
        [TestCase(100, 300)]
        [TestCase(100, 500)]
        public void Should_Return_Withdraw_InvalidOperationException(decimal balance, decimal withdrawAmount)
        {
            //Arrange
            _account.Balance = balance;

            //Assume
            Assert.Throws<InvalidOperationException>(() => _account.Withdraw(withdrawAmount));
        }

        [TestCase(200, 0, 50, 150, -50)]
        [TestCase(100, 100, 50, 50, 50)]
        public void Should_Withdraw_Amount_Be_Taken_Correctly(decimal balance, decimal withdrawn, decimal withdrawAmount, decimal expectedBalance, decimal expectedWithdrawn)
        {
            //Arrange
            _account.Balance = balance;
            _account.Withdrawn = withdrawn;

            //Assume
            _account.Withdraw(withdrawAmount);

            //Assert
            Assert.That(_account.Balance, Is.EqualTo(expectedBalance));
            Assert.That(_account.Withdrawn, Is.EqualTo(expectedWithdrawn));
        }

        [TestCase(3500, 1000)]
        [TestCase(3000, 3000)]
        [TestCase(4000, 1)]
        public void Should_Return_Pay_In_InvalidOperationException(decimal paidIn, decimal payInAmount)
        {
            //Arrange
            _account.PaidIn = paidIn;

            //Assume
            Assert.Throws<InvalidOperationException>(() => _account.PayIn(payInAmount));
        }

        [TestCase(100, 100, 50, 150, 150)]
        [TestCase(1000, 0, 500, 1500, 500)]
        public void Should_Pay_In_Amount_Be_Taken_Correctly(decimal balance, decimal paidIn, decimal payInAmount, decimal expectedBalance, decimal expectedPaidIn)
        {
            //Arrange
            _account.Balance = balance;
            _account.PaidIn = paidIn;

            //Assume
            _account.PayIn(payInAmount);

            //Assert
            Assert.That(_account.Balance, Is.EqualTo(expectedBalance));
            Assert.That(_account.PaidIn, Is.EqualTo(expectedPaidIn));
        }
    }
}