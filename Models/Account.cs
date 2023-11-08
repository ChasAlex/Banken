using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banken.Models
{
    internal class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public virtual User User { get; set; } 




    }
}
