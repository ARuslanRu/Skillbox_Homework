using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13.ViewModel
{
    class TransferBetweenAccountsViewModel : BaseViewModel
    {

        #region fields
        private decimal amount;
        private Account selectedFromAccount;
        private Account selectedToAccount;

        private IEnumerable<Account> fromAccounts;
        private IEnumerable<Account> toAccounts;
        #endregion

        #region constructors
        public TransferBetweenAccountsViewModel(Client client)
        {
            //TODO: Сперва сделать открытие счетов и вкладов 
        }

        #endregion

        #region properties
        public decimal Amount
        {
            get { return amount; }
            set
            {
                this.amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public Account SelectedFromAccount
        {
            get { return selectedFromAccount; }
            set
            {
                this.selectedFromAccount = value;
                OnPropertyChanged("SelectedFromAccount");
            }
        }

        public Account SelectedToAccount
        {
            get { return selectedToAccount; }
            set
            {
                this.selectedToAccount = value;
                OnPropertyChanged("SelectedToAccount");
            }
        }

        public IEnumerable<Account> FromAccounts
        {
            get { return fromAccounts; }
            set
            {
                this.fromAccounts = value;
                OnPropertyChanged("FromAccounts");
            }
        }

        public IEnumerable<Account> ToAccounts
        {
            get { return toAccounts; }
            set
            {
                this.toAccounts = value;
                OnPropertyChanged("ToAccounts");
            }
        }
        #endregion

    }
}
