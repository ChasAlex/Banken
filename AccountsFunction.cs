using Banken.Data;
using Banken.Models;
using Banken.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banken
{
    internal static class AccountsFunction
    {
        
        public static void OpenAccount(User user, BankContext context)
        {

            Console.WriteLine($"Skapar ett nytt konto för: {user.Name}");
            Console.Write("Kontonamn: ");
            string account_name = Console.ReadLine();

            Account newAccount = new Account()
            {
                
                Name = account_name,
                UserId = user.Id,
                Balance = 0,
            };

            bool success = DbHelper.AddAccount(context, newAccount);

            if(success)
            {
                Console.WriteLine($"Created account: {account_name}");
                
            }
            else {
                Console.WriteLine("There was an error creating your account :(");
                
                    }


        }


        public static void AddMoney(User user, BankContext context)
        {
            List<Account> accounts_list = DbHelper.GetAccountsByUserId(context,user.Id);
            foreach (Account account in accounts_list) {

                Console.WriteLine($"Account name: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance} SEK");
                Console.WriteLine();


            }
            Console.Write("Please enter the account name you want to deposit into: ");
            string accountToAddMoney = Console.ReadLine();

            foreach (Account account in accounts_list)
            {
                if(accountToAddMoney == account.Name)
                {
                    Console.Write("How much would you like to deposit?");
                    double amount = double.Parse(Console.ReadLine());
                    DbHelper.AddMoneyToAccount(context,user.Id,accountToAddMoney,amount);
                    
                   
                    return;
                }

            }
            




           
        }




        public static void RemoveMoney(User user, BankContext context)
        {
            List<Account> accounts_list = DbHelper.GetAccountsByUserId(context, user.Id);
            foreach (Account account in accounts_list)
            {

                Console.WriteLine($"Kontonamn: {account.Name}");
                Console.WriteLine($"Saldo: {account.Balance}");
                Console.WriteLine();


            }
            Console.Write("Ange vilket konto du vill ta ut pengar från: ");
            string accountToAddMoney = Console.ReadLine();

            foreach (Account account in accounts_list)
            {
                if (accountToAddMoney == account.Name)
                {
                    Console.Write("Hur mycket vill du ta ut?");
                    double amount = double.Parse(Console.ReadLine());
                    DbHelper.RemoveMoneyToAccount(context, user.Id, accountToAddMoney, amount);

                    
                    return;
                }

            }
            





        }


        public static void SeeAccountsAndBalance(User user,BankContext context)
        {
            
             Console.WriteLine($"Accounts and Balances for {user.Name}:");
            List<Account> acc = DbHelper.GetAccountsByUserId(context, user.Id);
             foreach (var account in acc)
                {
                    Console.WriteLine($"Account ID: {account.Id}, Name: {account.Name}, Balance: {account.Balance}");
                }

            if (acc.IsNullOrEmpty())
            {
                Console.Clear();
                Console.WriteLine("This user have no accounts.");
                return;
            }
        }



        public static void TransferFunds(User user,BankContext context)
        {
            //Retrieves accounts linked to user
            List<Account> accounts_list = DbHelper.GetAccountsByUserId(context, user.Id);

            foreach (Account account in accounts_list)
            {

                Console.WriteLine($"Account name: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance} SEK");
                Console.WriteLine();


            }

            Console.Write("What account would you like to transfer from: ");
            string acc_transfer_from = Console.ReadLine();
            Console.Write("What account would you like to transfer to: ");
            string acc_transfer_to = Console.ReadLine();
            Console.Write("How much would you like to transfer: ");
            double amount;
            
            double.TryParse(Console.ReadLine(),out amount);

            if(amount == 0)
            {
                Console.WriteLine("Cannot transfer 0 between accounts");
                return;
            }


            if(DbHelper.TransferFundsInDb(context,user.Id,acc_transfer_from,acc_transfer_to,amount) == true)
            {
                Console.WriteLine($"Succesfully transfered {amount.ToString()} from {acc_transfer_from} to {acc_transfer_to} ");
            }
            else { Console.WriteLine("There was an error in handling your transfer..."); }
            




        }










    }
}
