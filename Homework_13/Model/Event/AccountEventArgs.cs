using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13.Model.Event
{
    public class AccountEventArgs
    {
        // Сообщение
        public string Message { get; }
        // Сумма перевода
        public decimal Amount { get; }

        public AccountEventArgs(string mes, decimal amount)
        {
            Message = mes;
            Amount = amount;
        }
    }
}
