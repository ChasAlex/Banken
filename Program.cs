namespace Banken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bank!");
            Console.WriteLine("Please login here");

            Console.Write("Enter user name:");
            string userName = Console.ReadLine();
            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine();

            if(userName == "admin")
            {
                if(pin != "1234")
                {
                    Console.WriteLine("Wrong Password");
                    return;
                }
                AdminFunctions.DoAdminTasks();
                return;

            }
            //Code here for user login


        }
    }
}