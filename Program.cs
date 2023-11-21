using Banken.Models;

namespace Banken
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool Is_runing = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Bank!");
                Console.WriteLine("Please login here");

                Console.Write("Enter user name:");
                string userName = Console.ReadLine();

                Console.Write("Enter PIN: ");
                string pin = Console.ReadLine();



                User user1 = UserFunction.CheckUser(userName, pin);
                UserFunction.Menu_Logged(user1);


            } while (Is_runing);
        }
    }
}