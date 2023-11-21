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
    public static User CheckUser(string userName, string pin)
    {
        using (BankContext context = new BankContext())
        {
            Console.WriteLine("Logging in...");

            User userToReturn = null;

            for (int i = 0; i < 3; i++)
            {
                userToReturn = DbHelper.UserGetUserWithPin(context, userName, pin);

                if (userName == "admin" && pin == "1234")
                {
                    AdminFunctions.DoAdminTasks();
                    return null;

                }

                if (userToReturn != null)
                {
                    return userToReturn;
                }
                else
                {
                    Console.WriteLine($"Attempt: {i + 2}. Please try again.");
                    Console.Write("Enter user name: ");
                    userName = Console.ReadLine();
                    Console.Write("Enter Pin: ");
                    pin = Console.ReadLine();
                }
            }

            Console.WriteLine("Too many login attempts. Exiting...");
            Environment.Exit(0);

            return null;
        }
    }

    public static void Menu_Logged(User user)
    {
        if (user != null)
        {
            Console.Clear();
            Console.WriteLine($"{user.Name} was logged in");

            string text_menu = ("Menu:\n" +
                                "1. View your accounts and balances\n" +
                                "2. Transfer between accounts\n" +
                                "3. Withdraw money\n" +
                                "4. Deposit money\n" +
                                "5. Open a new account\n" +
                                "6. Log out");

            bool Is_running = true;

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


                                AccountsFunction.SeeAccountsAndBalance(user, context);
                                Console.WriteLine("Press enter to return to menu...");
                                Console.ReadLine();

                                break;
                            case 2:
                                AccountsFunction.TransferFunds(user, context);
                                Console.WriteLine("Press enter to return to menu...");
                                Console.ReadLine();




                                break;
                            case 3:
                                Console.WriteLine("Withdrawing money...");

                                AccountsFunction.RemoveMoney(user, context);
                                Console.WriteLine("Press enter to return to menu...");
                                Console.ReadLine();

                                break;
                            case 4:
                                AccountsFunction.AddMoney(user, context);
                                Console.WriteLine("Press enter to return to menu...");
                                Console.ReadLine();
                                break;
                            case 5:
                                AccountsFunction.OpenAccount(user, context);
                                Console.WriteLine("Press enter to return to menu...");
                                Console.ReadLine();
                                break;
                            case 6:
                                Console.WriteLine("Logging out");
                                Console.Clear();
                                Is_running = false;

                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a valid number.");

                                break;
                        }
                    }
                    else
                    {
                        // Parsing failed
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }
                }
            } while (Is_running);
        }
    }
}
