using System;

namespace Banks.Entities.BankAccount
{
    public interface IBankAccount
    {
        public Transaction AddMoneyToAccount(float money);

        public Transaction GetMoneyFromAccount(float money);
    }
}