using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13.Model
{
    class Deposit : IDeposit
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }


        public decimal Balance { get; set; }
    }
}
