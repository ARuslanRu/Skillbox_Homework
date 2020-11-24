using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_13.Model
{
    public class Account : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        //public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        //public decimal Balance { get; set; }

        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
