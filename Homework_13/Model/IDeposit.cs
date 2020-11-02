using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13.Model
{
    interface IDeposit
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
