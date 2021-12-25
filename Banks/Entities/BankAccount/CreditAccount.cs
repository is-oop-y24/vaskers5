using Banks.Tools;

namespace Banks.Entities.BankAccount
{
    public class CreditAccount : BaseBankAccount
    {
        public CreditAccount(
            int id,
            Client bankClient,
            BalanceInterest accountBalanceInterest,
            BankCommission commission,
            BankLimit limit)
            : base(id, bankClient, accountBalanceInterest, commission, limit)
        {
        }

        public override Transaction AddMoneyToAccount(float money)
        {
            return Money < 0 ? base.AddMoneyToAccount(money - (Commission.CommissionValue * money)) : base.AddMoneyToAccount(money);
        }

        public override Transaction GetMoneyFromAccount(float money)
        {
            if (Money < 0 && Money - money > -Limit.LimitValue)
            {
                return base.AddMoneyToAccount(money - (Commission.CommissionValue * money));
            }
            else if (Money - money <= -Limit.LimitValue)
            {
                throw new LimitException("You have reached your credit limit");
            }
            else
            {
                return base.GetMoneyFromAccount(money);
            }
        }
    }
}