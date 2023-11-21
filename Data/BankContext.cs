using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Banken.Models;

namespace Banken.Data
{
    internal class BankContext:DbContext
    {   
        public DbSet<User> Users { get; set; }
        public DbSet<Account>Accounts { get; set; }
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=Bank;Integrated Security=True;Pooling=False");
            
        }
    }
}
