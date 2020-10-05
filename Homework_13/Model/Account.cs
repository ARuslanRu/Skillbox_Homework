using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13.Model
{
    class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
    }
}
