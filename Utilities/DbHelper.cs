using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Banken.Data;
using Banken.Models;

namespace Banken.Utilities
{
    internal class DbHelper
    {
        // Method to retrieve all users from the database
        public static List<User> GetAllUsers(BankContext context)
        {
            List<User> users = context.Users.ToList();
            return users;
        }

        // Method to retrieve a user based on username and PIN
        public static User UserGetUserWithPin(BankContext context, string username, string pin)
        {
            try
            {
                User user = context.Users
                    .FirstOrDefault(u => u.Name == username && u.Pin == pin);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user: {ex.Message}");
                return null;
            }
        }

        // Method to add money to a specific account
        public static void AddMoneyToAccount(BankContext context, int userId, string name, double amount)
        {
            // Find the account based on the specified conditions
            Account accountToAdd = context.Accounts
                .FirstOrDefault(account => account.UserId == userId && account.Name == name);

            if (accountToAdd != null)
            {
                // Update the balance
                accountToAdd.Balance += amount;

                // Save changes to the database
                context.SaveChanges();

                Console.WriteLine($"Added {amount} to account {accountToAdd.Name}. New balance: {accountToAdd.Balance}");
            }
            else
            {
                Console.WriteLine($"Account not found for user ID {userId} and name {name}");
            }
        }

        // Method to remove money from a specific account
        public static void RemoveMoneyToAccount(BankContext context, int userId, string name, double amount)
        {
            // Find the account based on the specified conditions
            Account accountToRemove = context.Accounts
                .FirstOrDefault(account => account.UserId == userId && account.Name == name);
            

            
           





                if (accountToRemove != null)
                {
                    // Update the balance
                    accountToRemove.Balance -= amount;

                    // Save changes to the database
                    context.SaveChanges();

                    Console.WriteLine($"Withdrew {amount} from account {accountToRemove.Name}. New balance: {accountToRemove.Balance}");
                }
                else
                {
                    Console.WriteLine($"Account not found for user ID {userId} and name {name}");
                }
            
            
        }

        // Method to retrieve all accounts associated with a user
        public static List<Account> GetAccountsByUserId(BankContext context, int userid)
        {
            try
            {
                List<Account> accounts = context.Accounts
                    .Where(account => account.UserId == userid)
                    .ToList();

                return accounts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving accounts: {ex.Message}");
                return null;
            }
        }

        // Method to add a new user to the database
        public static bool AddUser(BankContext context, User user)
        {
            context.Users.Add(user);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding user: {e}");
                return false;
            }
            return true;
        }

        // Method to add a new account to the database
        public static bool AddAccount(BankContext context, Account account)
        {
            context.Accounts.Add(account);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding account: {e}");
                return false;
            }
            return true;
        }

        // Method to transfer funds between accounts in the database
        public static bool TransferFundsInDb(BankContext context, int userId, string name_remove, string name_add, double amount)
        {
            try
            {
                Account accountToAdd = context.Accounts
                    .FirstOrDefault(account => account.UserId == userId && account.Name == name_add);

                Account accountToRemove = context.Accounts
                    .FirstOrDefault(account => account.UserId == userId && account.Name == name_remove);

                if (accountToRemove.Balance < amount) 
                {
                    Console.WriteLine("The account you tried to transfer from does not have that amount");
                    return false;
                }

                if (accountToAdd != null && accountToRemove != null)
                {
                    accountToAdd.Balance += amount;
                    accountToRemove.Balance -= amount;

                    double newBalanceRemoved = accountToRemove.Balance;
                    double newBalanceAdded = accountToAdd.Balance;
                    
                }
                else
                {
                    return false;
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error transferring funds: {e}");
                return false;
            }
            return true;
        }
    }
}
