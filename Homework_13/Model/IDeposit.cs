using System;

namespace Homework_13.Model
{
    interface IDeposit : IIdentity
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Balance { get; set; }
    }
}
