﻿using Homework_13.Model.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_13.Model
{
    public class Account : INotifyPropertyChanged, IIdentity
    {
        public delegate void AccountHandler(object sender, AccountEventArgs e);
        private event AccountHandler notify; 
        public event AccountHandler Notify
        {
            add
            {
                if (notify != null) return;
                notify += value;
            }
            remove
            {
                notify -= value;
            }
        }


        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CreateDate { get; set; }

        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public bool SendTo(Account recipient, decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            if (amount > Balance)
            {
                return false;
            }

            notify?.Invoke(this, new AccountEventArgs($"Со счета {this.Id} на счет {recipient.Id} переведена суммма {amount}", amount));
            recipient.Balance += amount;
            this.Balance -= amount;

            return true;
        }
    }
}
