using System.Collections.Generic;
using Banks.Entities;

namespace Banks.Services
{
    public class CentralBank
    {
        private int _lastBankId = 0;

        public CentralBank()
        {
            SystemBanks = new List<SimpleBank>();
        }

        private List<SimpleBank> SystemBanks { get; set; }

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
    }
}