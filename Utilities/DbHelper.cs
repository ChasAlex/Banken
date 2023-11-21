using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Banken.Data;
using Banken.Models;

namespace Banken.Utilities
{
    internal class DbHelper
    {
        public static List<User>GetAllUsers(BankContext context)
        {
            List<User> users = context.Users.ToList();
            return users;
        }

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


        public static void RemoveMoneyToAccount(BankContext context, int userId, string name, double amount)
        {
            // Find the account based on the specified conditions
            Account accountToAdd = context.Accounts
                .FirstOrDefault(account => account.UserId == userId && account.Name == name);

            if (accountToAdd != null)
            {
                // Update the balance
                accountToAdd.Balance -= amount;

                // Save changes to the database
                context.SaveChanges();

                Console.WriteLine($"Withdrew {amount} to account {accountToAdd.Name}. New balance: {accountToAdd.Balance}");
            }
            else
            {
                Console.WriteLine($"Account not found for user ID {userId} and name {name}");
            }
        }





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


        public static bool AddUser(BankContext context,User user)
        {
            context.Users.Add(user);
            try
            {
                context.SaveChanges();
            }catch (Exception e)
            {
                Console.WriteLine($"Error adding user: {e}");
                return false;
            }
            return true;
        }

        public static bool AddAccount(BankContext context,Account account) 
        {
            
            context.Accounts.Add(account);
            try 
            {

                context.SaveChanges();
            }catch(Exception e)
            {
                Console.WriteLine($"Error adding account: {e}");
                return false;
            }
            return true;
        
        
        }


        public static bool TransferFundsInDb(BankContext context, int userId, string name_remove, string name_add, double amount)
        {
            
            try
            {
                Account accountToAdd = context.Accounts
                .FirstOrDefault(account => account.UserId == userId && account.Name == name_add);

                Account accountToRemove = context.Accounts
                .FirstOrDefault(account => account.UserId == userId && account.Name == name_remove);
                if (accountToAdd != null && accountToRemove != null)
                {
                    accountToAdd.Balance += amount;
                    accountToRemove.Balance -= amount;
                }
                else
                {
                    return false;
                }
            
                context.SaveChanges();

            }catch( Exception e)
            {
                Console.WriteLine($"Error transfering funds: {e}");
                return false;
            }
            return true;






            



        }




    }
}
