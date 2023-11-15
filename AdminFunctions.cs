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
                



                bool Is_running = true; 



                while (Is_running)
                {
                    Console.Clear();
                    Console.WriteLine("Current users in system:");
                    List<User> users = DbHelper.GetAllUsers(context);

                    foreach (User user in users)
                    {
                        Console.WriteLine($"Username: {user.Name}");
                        Console.WriteLine($"Pin: {user.Pin}");
                        Console.WriteLine($"Accounts: {user.Accounts}");
                        Console.WriteLine();

                    }
                    
                    Console.WriteLine($"Total number of users: {users.Count()}");
                    Console.WriteLine("c to Create a new user");
                    Console.WriteLine("x to exit");
                    Console.WriteLine("d to Delete");




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
                            Console.Clear();
                            return;




                        case "d":

                        
                            Console.WriteLine("Enter the name of the user to delete:");
                            string userNameToDelete = Console.ReadLine();

                            // Find the user to delete
                            User userToDelete = users.FirstOrDefault(u => u.Name.ToLower() == userNameToDelete.ToLower());

                            if (userToDelete != null)
                            {   
                                Console.WriteLine($"Name: {userToDelete.Name}");
                                Console.WriteLine($"Pin: {userToDelete.Pin}");
                                Console.Write("Enter 'yes' to confirm deletion: ");
                                string confirmation = Console.ReadLine();

                                if (confirmation.ToLower() == "yes")
                                {
                                    // Remove the user from the context
                                    context.Users.Remove(userToDelete);

                                    // Save changes to apply the deletion
                                    context.SaveChanges();

                                    Console.WriteLine($"User {userNameToDelete} deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Deletion canceled.");
                                    
                                }
                            }
                            else
                            {
                                Console.WriteLine($"User with name {userNameToDelete} not found.");
                            }
                            break;


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
