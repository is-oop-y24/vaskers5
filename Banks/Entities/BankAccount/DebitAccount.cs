using Banks.Tools;

namespace Banks.Entities.BankAccount
{
    public class DebitAccount : BaseBankAccount
    {
        private float _money;

        public DebitAccount(
            int id,
            Client bankClient,
            BalanceInterest accountBalanceInterest,
            BankCommission commission,
            BankLimit limit)
            : base(id, bankClient, accountBalanceInterest, commission, limit)
        {
        }

        public override float Money
        {
            get => _money;
            set
            {
                if (value < 0)
                    throw new MoneyException("Money on debit account cannot be < 0");
                _money = value;
            }
        }
    }
}