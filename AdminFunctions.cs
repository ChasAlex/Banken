using Banken.Data;
using Banken.Models;
using Banken.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banken
{
    internal static class AdminFunctions
    {
        public static void DoAdminTasks()
        {
            using(BankContext context = new BankContext()) {
                Console.WriteLine("Current users in system:");
                List<User> users = DbHelper.GetAllUsers(context);

                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Name}");

                }
                Console.WriteLine($"Total number of users: {users.Count()}");
                Console.WriteLine("c to Create a new user");
                Console.WriteLine("x to exit");
                
                while (true)
                {
                    Console.Write("Enter command:");
                    string command = Console.ReadLine();

                    switch(command.ToLower())
                    {
                        case "c":
                            // Create user
                            CreateUser(context);
                            break;

                        case "x":
                            //Exit
                            return;
                            

                        default:
                            Console.WriteLine($"Unknown command{command}");
                            break;
                    }
                }


            }
            
        }

        private static void CreateUser(BankContext context)
        {
            Console.WriteLine("Create user");
            Console.Write("Enter user name:");
            string username = Console.ReadLine();

            Random random = new Random();
            string pin = random.Next(1000, 10000).ToString();

            User newUser = new User()
            {
                Name = username,
                Pin = pin,
            };

            bool success = DbHelper.AddUser(context,newUser);

            if(success)
            {
                Console.WriteLine($"Created user {username} with pin {pin}");
            }else {


                Console.WriteLine("User created failed");
               }

        }

        
    }
}
