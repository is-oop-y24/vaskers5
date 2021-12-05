using System;
using Banks.Entities.BankAccount;
using Banks.Services;

namespace Banks
{
    internal static class ClientConsoleInterface
    {
        private static void Main()
        {
            var centralBank = new CentralBank();
            var bank1 = centralBank.CreateBank("Tinkoff", 3.5f, 4.0f, 1.5f, 50000, 5000000, 5);
            var bank2 = centralBank.CreateBank("Sber", 0, 3.5f, 2.5f, 40000, 5000000, 10);
            var b1Client1 = bank1.AddBankClient("Misha", false, "spb", 1234);
            var b1Client2 = bank1.AddBankClient("Vika", false);
            var b2Client1 = bank2.AddBankClient("Misha", true, "Parnas", 1234);
            var b2Client2 = bank2.AddBankClient("Lesha", true, "spn", 1234);

            // while (true)
            // {
            //     string userInput = Console.ReadLine();
            //
            //     switch (userInput)
            //     {
            //         case "Reg in bank":
            //         {
            //             Console.WriteLine("Which bank you want to register? Please write Name of this bank");
            //             string bankName = Console.ReadLine();
            //             var bank = centralBank.FindBankByName(bankName);
            //             if (bank is null) continue;
            //             Console.WriteLine("You need to write your Name, Do you want a notifications, your address and passport number.");
            //             string name = Console.ReadLine();
            //             bool notifications = Convert.ToBoolean(Console.ReadLine());
            //             string address = Console.ReadLine();
            //             int passportNumber = Convert.ToInt32(Console.ReadLine());
            //             Console.WriteLine("Which type account you want to use? Please write 0 for debit, 1 for deposit, 2 for Credit.");
            //             string accountRegisterType = Console.ReadLine();
            //             var client = bank.AddBankClient(name, notifications, address, passportNumber);
            //             switch (accountRegisterType)
            //             {
            //                 case "0":
            //                     var account = bank.AddDebitAccountForClient(client);
            //                     break;
            //                 case "1":
            //                     var account = bank.AddDepositAccountForClient(client);
            //                 case "2":
            //                     var account = bank.AddCreditAccountForClient(client);
            //             }
            //         }
            //
            //         case "Close program":
            //             break;
            //     }
            // }
        }
    }
}
