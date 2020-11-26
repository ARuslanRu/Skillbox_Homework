using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_13.Model
{
    class DepositWithCapitalization : IDeposit, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }


        private decimal balance;
        public decimal Balance
        {
            get
            {
                double percentRate = 0.12;
                int part = 12;
                int monthsPassed = MonthDifference(DateTime.Now, CreateDate);
                return balance * (decimal)Math.Pow(1 + percentRate / part, part * monthsPassed / part);
            }
            set
            {
                this.balance = value;
                OnPropertyChanged();
            }
        }
        
        private int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
