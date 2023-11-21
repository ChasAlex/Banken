using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banken.Data;
using Banken.Models;
using Banken.Utilities;

namespace Banken
{
    internal static class UserFunction 
    {
        public static User CheckUser(string userName, string pin)
        {
            using (BankContext context = new BankContext())
            {
                Console.WriteLine("Loggar...");
                

                List<User> users = DbHelper.GetAllUsers(context);
                int try_counter = 0;
                bool Is_running = true;
                do {
                    foreach (User user in users)
                    {
                        if (userName.ToLower() == user.Name.ToLower() && pin == user.Pin)
                        {
                            Console.WriteLine($"{user.Name} loggade in...");
                            
                            return user;
                        }
                        else if (userName.ToLower() == "admin" && pin == "1234")
                        {
                            AdminFunctions.DoAdminTasks();
                            return null;
                        }
                        
                    }

                    try_counter++;
                    Console.WriteLine($"Inloggning misslyckad, Försök: {try_counter.ToString()}");
                    Console.Write("Enter Username: ");
                    userName = Console.ReadLine();
                    Console.Write("Enter Pin: ");
                    pin = Console.ReadLine();
                    if (try_counter >= 3)
                    {
                        
                        Is_running = false;
                        
                    }
                } while (Is_running);

                // Additional logic if no user is found
                Console.WriteLine("För många misslyckad försök, Systemet stängs ner...");
                Environment.Exit(0);

                return null; // Return null if no user is found
            }
        }

        public static void Menu_Logged(User user)
        {
            //Account account = user.Accounts.FirstOrDefault();
            if (user != null) { 
            
            

            string text_menu =("Meny:\n" +
                        "1. Se dina konton och saldo\n" +
                        "2. Överföring mellan konton\n" +
                        "3. Ta ut pengar\n" +
                        "4. Sätt in pengar\n" +
                        "5. Öppna nytt konto\n" +
                        "6. Logga ut");

            bool Is_running = true;

            do{
                    Console.Clear();
                    Console.WriteLine($"{user.Name} är inloggad");
                    Console.WriteLine(text_menu);
                    Console.Write("Vänligen mata in:");
                    string user_input = Console.ReadLine();
                    int parsedNumber;

                    if (int.TryParse(user_input, out parsedNumber))
                    {
                        // Parsing succeeded
                        switch (parsedNumber)
                        {
                            case 1:
 
                                Console.WriteLine("Ser över dina konto");

                                break;
                            case 2:
                                Console.WriteLine("Överföring mellan konton");
                                AccountFunctions.TransferFunds(user);
                                break;
                            case 3:
                                Console.WriteLine("Ta ut pengar");
                                break;
                            case 4:
                                Console.WriteLine("Sätt in pengar");
                                break;
                            case 5:
                                Console.WriteLine("Öppna nytt konto");
                                break;
                            case 6:
                                Console.WriteLine("Loggar ut");
                                Console.Clear();
                                Is_running = false;

                                break;





                        }
                    }
                    else
                    {
                        // Parsing failed
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }

                } while (Is_running);
            





        }


    }
    }
}


