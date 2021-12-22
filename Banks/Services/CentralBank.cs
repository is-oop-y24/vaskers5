using System.Collections.Generic;
using System.Linq;
using Banks.Entities;

namespace Banks.Services
{
    public class CentralBank
    {
        private const int BankLimit = 0;
        private int _lastBankId = 0;

        public CentralBank()
        {
            SystemBanks = new List<SimpleBank>();
        }

        public List<SimpleBank> SystemBanks { get; set; }

        public SimpleBank CreateBank(
            string bankName,
            float bankBalanceInterest,
            float bankDepositInterest,
            float bankCommission,
            float bankLimit,
            float bankCreditLimit,
            int depositAccountDuration)
        {
            var bank = new SimpleBank(
                _lastBankId++,
                bankName,
                new BalanceInterest(bankBalanceInterest),
                new BalanceInterest(bankDepositInterest),
                new BankCommission(bankCommission),
                new BankLimit(bankLimit),
                new BankLimit(bankCreditLimit),
                depositAccountDuration);
            SystemBanks.Add(bank);
            return bank;
        }

        public void RunTimeMachine(int dayNumber)
        {
            for (int i = 0; i < dayNumber; i++)
            {
                foreach (SimpleBank bank in SystemBanks) bank.AddAccountInterests();
                if (i % BankLimit != 0) continue;
                {
                    foreach (SimpleBank bank in SystemBanks) bank.AddAccountInterestOnAccount();
                }
            }
        }

        public SimpleBank FindBankByName(string bankName)
        {
            return SystemBanks.FirstOrDefault(bank => bank.BankName == bankName);
        }
    }
}