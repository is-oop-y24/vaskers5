using System;

namespace Banks.Entities.BankAccount
{
    public interface IBankAccount
    {
        Transaction AddMoneyToAccount(float money);

        Transaction GetMoneyFromAccount(float money);
    }
}