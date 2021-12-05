using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Entities.BankAccount;
using Banks.Tools;
namespace Banks.Services
{
    public class SimpleBank
    {
        private int _lastAccountId = 0;

        public SimpleBank(
            int id,
            string bankName,
            BalanceInterest bankBalanceInterest,
            BalanceInterest bankDepositBalanceInterest,
            BankCommission bankCommission,
            BankLimit bankLimit,
            BankLimit creditLimit,
            int depositAccountDuration)
        {
            Id = id;
            _lastAccountId = id;
            BankName = bankName;
            BankBalanceInterest = bankBalanceInterest;
            BankDepositBalanceInterest = bankDepositBalanceInterest;
            BankCommission = bankCommission;
            BankAccountLimit = bankLimit;
            CreditLimit = creditLimit;
            DepositAccountDuration = depositAccountDuration;
            BankClients = new List<Client>();
            Accounts = new List<BaseBankAccount>();
        }

        public int Id { get; }
        public string BankName { get; set; }
        public BalanceInterest BankBalanceInterest { get; }
        public BalanceInterest BankDepositBalanceInterest { get; }
        public BankCommission BankCommission { get; }
        public BankLimit CreditLimit { get; }
        public BankLimit BankAccountLimit { get; }
        public int DepositAccountDuration { get; set; }
        private List<Client> BankClients { get; }
        private List<BaseBankAccount> Accounts { get; }

        public void TransferMoney(int firstAccountId, int secondAccountId, float value)
        {
            var firstAccount = FindAccountById(firstAccountId);
            var secondAccount = FindAccountById(secondAccountId);
            firstAccount.GetMoneyFromAccount(value);
            secondAccount.AddMoneyToAccount(value);
        }

        public void SendNotificationsForClients(string message)
        {
            IEnumerable<Client> clients = BankClients.Where(client => client.Notifications);
            foreach (Client client in clients)
            {
                client.NotificationsHistory.Add(message);
            }
        }

        public void ChangeBalanceInterest(float value)
        {
            BankBalanceInterest.InterestValue = value;
            SendNotificationsForClients($"Now we have {value} balance interest!");
        }

        public void ChangeDepositBalanceInterest(float value)
        {
            BankDepositBalanceInterest.InterestValue = value;
            SendNotificationsForClients($"Now we have {value} deposit balance interest!");
        }

        public void ChangeBankCommission(float value)
        {
            BankCommission.CommissionValue = value;
            SendNotificationsForClients($"Now we have {value} credit commission!");
        }

        public void ChangeBankCreditLimit(float value)
        {
            CreditLimit.LimitValue = value;
            SendNotificationsForClients($"Now we have {value} credit limit!");
        }

        public void ChangeBankAccountLimit(float value)
        {
            BankAccountLimit.LimitValue = value;
            SendNotificationsForClients($"Now we have {value} limit for your fishy accounts!");
        }

        public void ChangeDepositAccountDuration(int value)
        {
            DepositAccountDuration = value;
            SendNotificationsForClients($"Now we have {value} limit for your fishy accounts!");
        }

        public Client AddBankClient(string name, bool notifications, string address = default, int passportId = default)
        {
            var client = new Client(name, notifications, address, passportId);
            BankClients.Add(client);
            return client;
        }

        public DebitAccount AddDebitAccountForClient(Client client)
        {
            var clientAccount = new DebitAccount(_lastAccountId++, client, BankBalanceInterest, BankCommission, BankAccountLimit);
            Accounts.Add(clientAccount);
            return clientAccount;
        }

        public DepositAccount AddDepositAccountForClient(Client client, float startMoney)
        {
            var clientAccount = new DepositAccount(_lastAccountId++, client, BankDepositBalanceInterest, BankCommission, BankAccountLimit, DateTime.Now.AddMonths(DepositAccountDuration), startMoney);
            Accounts.Add(clientAccount);
            return clientAccount;
        }

        public CreditAccount AddCreditAccountForClient(Client client)
        {
            var clientAccount = new CreditAccount(_lastAccountId++, client, new BalanceInterest(0), BankCommission, BankAccountLimit);
            Accounts.Add(clientAccount);
            return clientAccount;
        }

        public void AddAccountInterests()
        {
            foreach (BaseBankAccount account in Accounts)
            {
                account.BalanceInterestPerMonth += account.Money * account.AccountBalanceInterest.InterestValue;
            }
        }

        public void AddAccountInterestOnAccount()
        {
            foreach (BaseBankAccount account in Accounts)
            {
                account.Money += account.BalanceInterestPerMonth;
                account.BalanceInterestPerMonth = 0;
            }
        }

        private BaseBankAccount FindAccountById(int accountId)
        {
            BaseBankAccount account = Accounts.FirstOrDefault(account => account.Id == accountId);
            if (account != null)
            {
                return account;
            }
            else
            {
                throw new FindAccountException($"We cannot find account with this {accountId} id!");
            }
        }
    }
}