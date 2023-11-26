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
        // Method to open a new account for a user
        public static void OpenAccount(User user, BankContext context)
        {
            Console.WriteLine($"Creating a new account for: {user.Name}");
            Console.Write("Account name: ");
            string account_name = Console.ReadLine();

            Account newAccount = new Account()
            {
                Name = account_name,
                UserId = user.Id,
                Balance = 0,
            };

            // Adding the new account to the database
            bool success = DbHelper.AddAccount(context, newAccount);

            if (success)
            {
                Console.WriteLine($"Created account: {account_name}");
            }
            else
            {
                Console.WriteLine("There was an error creating your account :(");
            }
        }

        // Method to deposit money into a user's account
        public static void AddMoney(User user, BankContext context)
        {
            // Retrieving the list of accounts linked to the user
            List<Account> accounts_list = DbHelper.GetAccountsByUserId(context, user.Id);

            foreach (Account account in accounts_list)
            {
                Console.WriteLine($"Account name: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance} SEK");
                Console.WriteLine();
            }

            Console.Write("Please enter the account name you want to deposit into: ");
            string accountToAddMoney = Console.ReadLine();

            foreach (Account account in accounts_list)
            {
                if (accountToAddMoney == account.Name)
                {
                    Console.Write("How much would you like to deposit?");
                    double amount = double.Parse(Console.ReadLine());
                    DbHelper.AddMoneyToAccount(context, user.Id, accountToAddMoney, amount);

                    return;
                }
            }
        }

        // Method to withdraw money from a user's account
        public static void RemoveMoney(User user, BankContext context)
        {
            // Retrieving the list of accounts linked to the user
            List<Account> accounts_list = DbHelper.GetAccountsByUserId(context, user.Id);

            foreach (Account account in accounts_list)
            {
                Console.WriteLine($"Account name: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance}");
                Console.WriteLine();
            }

            Console.Write("Please enter the account name you want to withdraw from: ");
            string accountToRemoveMoney = Console.ReadLine();

            foreach (Account account in accounts_list)
            {
                if (accountToRemoveMoney == account.Name)
                {
                    Console.Write("How much would you like to withdraw?");
                    double amount = double.Parse(Console.ReadLine());
                    DbHelper.RemoveMoneyToAccount(context, user.Id, accountToRemoveMoney, amount);

                    return;
                }
            }
        }

        // Method to display user accounts and balances
        public static void SeeAccountsAndBalance(User user, BankContext context)
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
                Console.WriteLine("This user has no accounts.");
                return;
            }
        }

        // Method to transfer funds between user accounts
        public static void TransferFunds(User user, BankContext context)
        {
            // Retrieving the list of accounts linked to the user
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

            double.TryParse(Console.ReadLine(), out amount);

            if (amount == 0)
            {
                Console.WriteLine("Cannot transfer 0 between accounts");
                return;
            }

            // Attempting to transfer funds between accounts in the database
            if (DbHelper.TransferFundsInDb(context, user.Id, acc_transfer_from, acc_transfer_to, amount) == true)
            {
                Console.WriteLine($"Successfully transferred {amount.ToString()} from {acc_transfer_from} to {acc_transfer_to} ");
            }
            else
            {
                Console.WriteLine("There was an error in handling your transfer...");
            }
        }
    }
}
