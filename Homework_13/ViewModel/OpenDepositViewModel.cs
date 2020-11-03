using Homework_13.Helper;
using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Homework_13.ViewModel
{
    class OpenDepositViewModel : BaseViewModel
    {
        private Account account;
        private decimal amount;
        private bool isWithCapitalization;

        public Account Account
        {
            get { return account; }
            set
            {
                this.account = value;
                OnPropertyChanged("Account");
            }
        }
        public decimal Amount
        {
            get { return amount; }
            set
            {
                this.amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public OpenDepositViewModel(Account account)
        {
            this.account = account;
        }

        private RelayCommand selectRadioButton;
        public RelayCommand SelectRadioButton
        {
            get
            {
                return selectRadioButton ??
                    (selectRadioButton = new RelayCommand(obj =>
                    {
                        isWithCapitalization = Boolean.Parse((string)obj);
                        //Debug.Print("isWithCapitalization: " + isWithCapitalization.ToString());
                    }));
            }
        }

        private RelayCommand confirmCommand;
        public RelayCommand СonfirmCommand
        {
            get
            {
                return confirmCommand ??
                    (confirmCommand = new RelayCommand(obj =>
                    {
                        
                    }));
            }
        }


    }
}
