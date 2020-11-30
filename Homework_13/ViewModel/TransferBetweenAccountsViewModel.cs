using Homework_13.Helper;
using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Homework_13.ViewModel
{
    class TransferBetweenAccountsViewModel : BaseViewModel, IDataErrorInfo
    {

        #region fields
        private string amount;
        private Client selectedRecipient;
        private Account senderAccount;
        private IEnumerable<Client> recipients;
        private string errorMessage;
        private RelayCommand confirmCommand;
        #endregion

        #region constructors
        public TransferBetweenAccountsViewModel(Account account)
        {
            SenderAccount = account;
            Recipients = Repository.Clients.Where(x => x.Id != SenderAccount.ClientId);
        }
        #endregion

        #region properties
        public string Amount
        {
            get { return amount; }
            set
            {
                this.amount = value;
                OnPropertyChanged();
            }
        }
        public Client SelectedRecipient
        {
            get { return selectedRecipient; }
            set
            {
                this.selectedRecipient = value;
                OnPropertyChanged();
            }
        }
        public Account SenderAccount
        {
            get { return senderAccount; }
            set
            {
                this.senderAccount = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Client> Recipients
        {
            get { return recipients; }
            set
            {
                this.recipients = value;
                OnPropertyChanged();
            }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                this.errorMessage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region commands

        public RelayCommand ConfirmCommand
        {
            get
            {
                return confirmCommand ??
                    (confirmCommand = new RelayCommand(obj =>
                    {
                        Account recipientAccount = Repository.Accounts.Where(x => x.ClientId == SelectedRecipient.Id).FirstOrDefault();
                        decimal amountDecimal = decimal.Parse(Amount);
                        SenderAccount.SendTo(recipientAccount, amountDecimal);
                        Window window = obj as Window;
                        window.Close();
                    },
                    obj => string.IsNullOrEmpty(ErrorMessage) && !string.IsNullOrEmpty(Amount) && SelectedRecipient != null));
            }
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Amount":
                        if (!string.IsNullOrEmpty(Amount))
                        {
                            if (decimal.TryParse(amount, out decimal result))
                            {
                                if (result <= 0)
                                {
                                    error = "Сумма должна быть больше 0";
                                }

                                if (result > SenderAccount.Balance)
                                {
                                    error = "Сумма превышает сумму на счете списания";
                                }
                            }
                            else
                            {
                                error = "Введены недопустимые символы";
                            }
                        }
                        break;

                }
                ErrorMessage = error;
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
