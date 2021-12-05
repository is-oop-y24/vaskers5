using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Banks.Tools;
using TransactionException = Banks.Tools.TransactionException;

namespace Banks.Entities.BankAccount
{
    public class BaseBankAccount : IBankAccount
    {
        private int _lastTransactionId = 0;
        public BaseBankAccount(int id, Client bankClient, BalanceInterest accountBalanceInterest, BankCommission commission, BankLimit limit)
        {
            Id = id;
            BankClient = bankClient;
            Money = 0;
            AccountBalanceInterest = accountBalanceInterest;
            Commission = commission;
            Limit = limit;
            CreationTime = DateTime.Now;
            TransactionHistory = new List<Transaction>() { };
            BalanceInterestPerMonth = 0;
        }

        public int Id { get; }
        public DateTime CreationTime { get; }
        public Client BankClient { get; }
        public virtual float Money { get; set; }

        public BalanceInterest AccountBalanceInterest { get; set; }
        public BankCommission Commission { get; set; }
        public BankLimit Limit { get; set; }
        public List<Transaction> TransactionHistory { get; set; }

        public float BalanceInterestPerMonth { get; set; }

        public virtual Transaction AddMoneyToAccount(float money)
        {
            var transaction = new Transaction(_lastTransactionId++, Money, Money + money);
            TransactionHistory.Add(transaction);
            Money += money;
            return transaction;
        }

        public virtual Transaction GetMoneyFromAccount(float money)
        {
            var transaction = new Transaction(_lastTransactionId++, Money, Money - money);
            TransactionHistory.Add(transaction);
            if (BankClient.IsFishy && money > Limit.LimitValue)
                throw new LimitException("You have exceeded your funds limit");
            Money -= money;
            return transaction;
        }

        public Transaction DeclineTransaction(int id)
        {
            Transaction transaction = TransactionHistory.FirstOrDefault(transaction => transaction.Id == id);
            if (transaction is null)
            {
                throw new TransactionException($"We can't find {id} transaction!");
            }
            else if (transaction.Declined)
            {
                throw new TransactionException($"This transaction already declined!");
            }

            transaction.Declined = true;
            Money = transaction.MoneyBefore;
            return transaction;
        }
    }
}