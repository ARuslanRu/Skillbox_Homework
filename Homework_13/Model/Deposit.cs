using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_13.Model
{
    class Deposit : IDeposit, INotifyPropertyChanged
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
                if(MonthDifference(DateTime.Now, CreateDate) < 12)
                {
                    return balance;
                }
                return balance + balance * 0.12m;
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
