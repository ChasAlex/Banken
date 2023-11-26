using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banken.Data;
using Banken.Models;
using Banken.Utilities;
using Banken;

internal static class UserFunction
{
    // Method to check user credentials and log in
    public static User CheckUser(string userName, string pin)
    {
        using (BankContext context = new BankContext())
        {
            Console.WriteLine("Logging in...");

            User userToReturn = null;

            // Allowing three login attempts
            for (int i = 0; i < 3; i++)
            {
                userToReturn = DbHelper.UserGetUserWithPin(context, userName, pin);

                // Allowing admin login with a specific username and pin
                if (userName == "admin" && pin == "1234")
                {
                    AdminFunctions.DoAdminTasks();
                    return null;
                }

                // If user credentials are correct, return the user
                if (userToReturn != null)
                {
                    return userToReturn;
                }
                else
                {
                    // Prompt user to try again if credentials are incorrect
                    Console.WriteLine($"Attempt: {i + 2}. Please try again.");
                    Console.Write("Enter user name: ");
                    userName = Console.ReadLine();
                    Console.Write("Enter Pin: ");
                    pin = Console.ReadLine();
                }
            }

            // If three login attempts fail, exit the program
            Console.WriteLine("Too many login attempts. Exiting...");
            Environment.Exit(0);

            return null;
        }
    }

    // Method to display the logged-in user menu
    public static void Menu_Logged(User user)
    {
        if (user != null)
        {
            Console.Clear();
            Console.WriteLine($"{user.Name} was logged in");

            // Menu options for the logged-in user
            string text_menu = ("Menu:\n" +
                                "1. View your accounts and balances\n" +
                                "2. Transfer between accounts\n" +
                                "3. Withdraw money\n" +
                                "4. Deposit money\n" +
                                "5. Open a new account\n" +
                                "6. Log out");

            bool Is_running = true;

            // Displaying the menu and handling user input
            do
            {
                Console.Clear();
                Console.WriteLine($"{user.Name} is logged in...");
                Console.WriteLine(text_menu);
                Console.Write("Please enter your choice:");
                string user_input = Console.ReadLine();
                int parsedNumber;

                using (BankContext context = new BankContext())
                {
                    if (int.TryParse(user_input, out parsedNumber))
                    {
                        // Parsing succeeded
                        switch (parsedNumber)
                        {
                            case 1:
                                // Displaying user accounts and balances
                                AccountsFunction.SeeAccountsAndBalance(user, context);
                                Console.WriteLine("Press enter to return to the menu...");
                                Console.ReadLine();
                                break;
                            case 2:
                                // Performing funds transfer between accounts
                                AccountsFunction.TransferFunds(user, context);
                                Console.WriteLine("Press enter to return to the menu...");
                                Console.ReadLine();
                                break;
                            case 3:
                                // Withdrawing money from an account
                                Console.WriteLine("Withdrawing money...");
                                AccountsFunction.RemoveMoney(user, context);
                                Console.WriteLine("Press enter to return to the menu...");
                                Console.ReadLine();
                                break;
                            case 4:
                                // Depositing money into an account
                                AccountsFunction.AddMoney(user, context);
                                Console.WriteLine("Press enter to return to the menu...");
                                Console.ReadLine();
                                break;
                            case 5:
                                // Opening a new account
                                AccountsFunction.OpenAccount(user, context);
                                Console.WriteLine("Press enter to return to the menu...");
                                Console.ReadLine();
                                break;
                            case 6:
                                // Logging out and exiting the menu loop
                                Console.WriteLine("Logging out");
                                Console.Clear();
                                Is_running = false;
                                break;
                            default:
                                // Handling invalid menu choices
                                Console.WriteLine("Invalid choice. Please enter a valid number.");
                                break;
                        }
                    }
                    else
                    {
                        // Handling invalid user input
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }
                }
            } while (Is_running);
        }
    }
}
