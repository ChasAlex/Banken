﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
