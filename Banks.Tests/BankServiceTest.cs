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
            var B1Client1 = bank1.AddBankClient("Misha", false, "Parnas", 1234);
            var B1Client2 = bank1.AddBankClient("Vika", false);
            var B2Client1 = bank2.AddBankClient("Misha", true, "Parnas", 1234);
            _centralBank = centralBank;
        }

        [Test]
        public void BankCreation()
        {
            var bank3 = _centralBank.CreateBank("Alpha", 0, 3.5f, 2.5f, 40000, 5000000, 10);
            Assert.False(bank3 is null);
        }
    }
        
        
}