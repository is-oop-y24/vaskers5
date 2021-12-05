using System;
using System.Data;
using Banks.Tools;

namespace Banks.Entities.BankAccount
{
    public class DepositAccount : BaseBankAccount
    {
        public DepositAccount(
            int id,
            Client bankClient,
            BalanceInterest accountBalanceInterest,
            BankCommission commission,
            BankLimit limit,
            DateTime durationEnd,
            float startMoney)
            : base(id, bankClient, accountBalanceInterest, commission, limit)
        {
            DurationEnd = durationEnd;
            Money = startMoney;
        }

        public DateTime DurationEnd { get; }

        public override Transaction AddMoneyToAccount(float money)
        {
            if (CreationTime < DurationEnd)
            {
                throw new DepositAccountDurationException("Your deposit account duration is not end.");
            }
            else
            {
                return base.AddMoneyToAccount(money);
            }
        }

        public override Transaction GetMoneyFromAccount(float money)
        {
            if (CreationTime < DurationEnd)
            {
                throw new DepositAccountDurationException("Your deposit account duration is not end.");
            }
            else
            {
                return base.GetMoneyFromAccount(money);
            }
        }
    }
}