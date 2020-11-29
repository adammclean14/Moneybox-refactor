using Moneybox.App;

using NUnit.Framework;
using System;

namespace MoneyBox.App.Testing
{
    [TestFixture]
    public class AccountTests
    {
        //private IAccountRepository _accountRepository;
        //private INotificationService _notificationService;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account();
        }


        [Test]
        public void CheckBalanceEnoughForTransferTest()
        {
            ////Arrange
            //var transferMoney = new TransferMoney(accountRepository, notificationService);
            //var fromAccountId = Guid.NewGuid();
            //var toAccountId = Guid.NewGuid();
            //var amount = -100m;

            //transferMoney.Execute(fromAccountId, toAccountId, amount);

            ////Act
            ////transferMoney.Execute(fromAccountId, toAccountId, amount);
            //var ex = Assert.Throws<InvalidOperationException>(() => transferMoney.Execute(fromAccountId, toAccountId, amount));

            ////Assert
            //Assert.That(ex.Message, Is.EqualTo("Insufficient funds to make transfer"));
        }
    }
}