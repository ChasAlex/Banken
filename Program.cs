using Banken.Models;

namespace Banken
{
    internal class Program
    {
        // Main method that serves as the entry point for the program
        static void Main(string[] args)
        {
            // Variable to control the main loop
            bool Is_running = true;

            // Main loop for user login and menu interactions
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Bank!");
                Console.WriteLine("Please log in here");

                // Prompting user for username and PIN
                Console.Write("Enter user name:");
                string userName = Console.ReadLine();

                Console.Write("Enter PIN: ");
                string pin = Console.ReadLine();

                // Checking user credentials and logging in
                User user1 = UserFunction.CheckUser(userName, pin);

                // Displaying the logged-in user menu
                UserFunction.Menu_Logged(user1);

            } while (Is_running);
        }
    }
}
