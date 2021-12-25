using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Banks.Entities;
using Banks.Entities.BankAccount;
using Banks.Tools;
using Banks.Services;
using NUnit.Framework.Constraints;


namespace Banks.Tests
{
    public class Tests
    {
        private CentralBank _centralBank; 
        [SetUp]
        public void SetUp()
        {
            var centralBank = new CentralBank();
            var bank1 = centralBank.CreateBank("Tinkoff", 3.5f, 4.0f, 1.5f, 50000, 5000000, 5);
            var bank2 = centralBank.CreateBank("Sber", 0, 3.5f, 2.5f, 40000, 5000000, 10);
            _centralBank = centralBank;
        }

        [Test]
        public void BankCreation()
        {
            var bank3 = _centralBank.CreateBank("Alpha", 0, 3.5f, 2.5f, 40000, 5000000, 10);
            Assert.False(bank3 is null);
        }
        
        [Test]
        public void TransferMoneyTest()
        {
            var bank1 = _centralBank.FindBankByName("Tinkoff");
            var B1Client1 = bank1.AddBankClient("Misha", false, "Parnas", 1234);
            var B1Client1Account = bank1.AddDebitAccountForClient(B1Client1);
            var B1Client2 = bank1.AddBankClient("Vika", false);
            var B1Client2Account = bank1.AddDebitAccountForClient(B1Client2);
            B1Client1Account.AddMoneyToAccount(5000);
            B1Client2Account.AddMoneyToAccount(10000);
            bank1.TransferMoney(B1Client1Account.Id, B1Client2Account.Id, 2000);
            Assert.AreEqual(B1Client1Account.Money, 3000);
            Assert.AreEqual(B1Client2Account.Money, 12000);
        }
        
        [Test]
        public void CreditAccountTest()
        {
            var bank1 = _centralBank.FindBankByName("Tinkoff");
            var B1Client1 = bank1.AddBankClient("Misha", false, "Parnas", 1234);
            var B1Client1Account = bank1.AddCreditAccountForClient(B1Client1);
            Assert.Catch<BanksException>(() =>
            {
                B1Client1Account.GetMoneyFromAccount(10000000);
            });
        }
        
        [Test]
        public void FishyAccountTest()
        {
            var bank1 = _centralBank.FindBankByName("Tinkoff");
            var B1Client1 = bank1.AddBankClient("Misha", false);
            var B1Client1Account = bank1.AddCreditAccountForClient(B1Client1);
            B1Client1Account.AddMoneyToAccount(600000000);
            Assert.Catch<BanksException>(() =>
            {
                B1Client1Account.GetMoneyFromAccount(50000000);
            });
        }
        
        [Test]
        public void DepositAccountTest()
        {
            var bank1 = _centralBank.FindBankByName("Tinkoff");
            var B1Client1 = bank1.AddBankClient("Misha", false);
            var B1Client1Account = bank1.AddDepositAccountForClient(B1Client1, 5000);
            Assert.Catch<BanksException>(() =>
            {
                B1Client1Account.GetMoneyFromAccount(500);
            });
        }
        [Test]
        public void DebitAccountTest()
        {
            var bank1 = _centralBank.FindBankByName("Tinkoff");
            var B1Client1 = bank1.AddBankClient("Misha", false);
            var B1Client1Account = bank1.AddDebitAccountForClient(B1Client1);
            Assert.Catch<BanksException>(() =>
            {
                B1Client1Account.GetMoneyFromAccount(500);
            });
        }
        
    }
        
        
}