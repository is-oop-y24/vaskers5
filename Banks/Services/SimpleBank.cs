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
            BankName = bankName;
            BankBalanceInterest = bankBalanceInterest;
            BankDepositBalanceInterest = bankDepositBalanceInterest;
            BankCommission = bankCommission;
            BankAccountLimit = bankLimit;
            CreditLimit = creditLimit;
            DepositAccountDuration = depositAccountDuration;
            BankClients = new List<Client>();
            BankDebitAccounts = new List<DebitAccount>();
            BankDepositAccounts = new List<DepositAccount>();
            BankCreditAccounts = new List<CreditAccount>();
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
        private List<DebitAccount> BankDebitAccounts { get; }
        private List<DepositAccount> BankDepositAccounts { get; }
        private List<CreditAccount> BankCreditAccounts { get; }

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
            BankDebitAccounts.Add(clientAccount);
            return clientAccount;
        }

        public DepositAccount AddDepositAccountForClient(Client client)
        {
            var clientAccount = new DepositAccount(_lastAccountId++, client, BankBalanceInterest, BankCommission, BankAccountLimit, DateTime.Now.AddMonths(DepositAccountDuration));
            BankDepositAccounts.Add(clientAccount);
            return clientAccount;
        }

        public CreditAccount AddCreditAccountForClient(Client client)
        {
            var clientAccount = new CreditAccount(_lastAccountId++, client, BankBalanceInterest, BankCommission, BankAccountLimit);
            BankCreditAccounts.Add(clientAccount);
            return clientAccount;
        }

        private dynamic FindAccountById(int accountId)
        {
            var debitAccount = BankDebitAccounts.FirstOrDefault(account => account.Id == accountId);
            var depositAccount = BankDepositAccounts.FirstOrDefault(account => account.Id == accountId);
            var creditAccount = BankCreditAccounts.FirstOrDefault(account => account.Id == accountId);
            if (debitAccount != null)
            {
                return debitAccount;
            }
            else if (depositAccount != null)
            {
                return depositAccount;
            }
            else if (creditAccount != null)
            {
                return creditAccount;
            }
            else
            {
                throw new FindAccountException($"We cannot find account with this {accountId} id!");
            }
        }
    }
}