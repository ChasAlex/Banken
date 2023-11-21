using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Banken.Data;
using Banken.Models;

namespace Banken
{
	internal class AccountFunctions 
	{
		public static void TransferFunds(User user)
		{
			//Retrieves accounts linked to user
			List<Account> accounts = user.Accounts.ToList();

			bool validInput = false;
			int choice = 0;
			int choice2 = 0;
			int counter = 0;
			
			while (validInput == false)
			{
				while (true)
				{
					//Prints all accounts in a menu-form
					foreach(Account acc in accounts)
					{
						counter++;
						Console.WriteLine($"{counter}) Account name: {acc.Name} Balance: {acc.Balance}");
					}

					//Take input from which account to transfer from
					Console.Write("Which account would you like to transfer from?: ");
					int.TryParse(Console.ReadLine(), out choice);

					//If invalid input from user
					if (choice > accounts.Count())
					{
						Console.Clear();
                        Console.WriteLine("Thats not a valid choice, please select from the accounts.\n");
						break;
                    }

					//Take input from which account to transfer to
					Console.Write("Which account would you like to transfer to?: ");
					int.TryParse(Console.ReadLine(), out choice2);

					//If invalid input from user
					if (choice2 > accounts.Count())
					{
						Console.Clear();
						Console.WriteLine("Thats not a valid choice, please select from the accounts.\n");
						break;
					}
					else if(choice2 == choice)
					{
						Console.Clear();
                        Console.WriteLine("You cannot transfer funds to the same account, please select two different accounts\n");
						break;
                    }
					validInput = true;
				}
			}

			//Print the chosen accounts
			Console.Clear();
			Console.WriteLine($"Transfer from account:{accounts[choice].Name} Funds: {accounts[choice].Balance}$");
			Console.WriteLine($"Transfer to account: {accounts[choice2].Name} Funds: {accounts[choice2].Balance}$");
            
			//Transfer from one account to the other
			Console.Write("\nHow much would you like to transfer: ");
			while (true)
			{
				if(double.TryParse(Console.ReadLine(), out double quantity) && quantity < accounts[choice].Balance)
				{
					accounts[choice].Balance = accounts[choice].Balance - quantity;
					accounts[choice2].Balance = accounts[choice2].Balance + quantity;
					break;
				}
				else
				{
                    Console.WriteLine("Please enter a valid amount");
                }
			}

			//Print the updated accounts
			Console.Clear();
            Console.WriteLine("Updated accounts:");
            Console.WriteLine($"Account Name: {accounts[choice].Name} funds: {accounts[choice].Balance}$");
			Console.WriteLine($"Account Name: {accounts[choice2].Name} funds: {accounts[choice2].Balance}$");

            Console.WriteLine("Press any key to return to the main menu...");
			Console.ReadKey();
        }
	}
}
