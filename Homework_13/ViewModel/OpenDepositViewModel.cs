using Homework_13.Helper;
using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace Homework_13.ViewModel
{
    class OpenDepositViewModel : BaseViewModel, IDataErrorInfo
    {
        private Account account;
        private string amount;
        private bool isWithCapitalization;
        private string errorMessage;

        public Account Account
        {
            get { return account; }
            set
            {
                this.account = value;
                OnPropertyChanged("Account");
            }
        }

        public string Amount
        {
            get { return amount; }
            set
            {
                this.amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                this.errorMessage = value;
                OnPropertyChanged("ErrorMessage");
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
                        if (decimal.TryParse(Amount, out decimal result))
                        {
                            IDeposit deposit;
                            if (isWithCapitalization)
                            {
                                deposit = new DepositWithCapitalization()
                                {
                                    Id = 0,
                                    ClientId = Account.ClientId,
                                    Name = "Депозит c капитализацией",
                                    Balance = result,
                                    CreateDate = DateTime.Now
                                };
                            }
                            else
                            {
                                deposit = new Deposit()
                                {
                                    Id = 0,
                                    ClientId = Account.ClientId,
                                    Name = "Депозит",
                                    Balance = result,
                                    CreateDate = DateTime.Now
                                };
                            }

                            Account.Balance -= result;
                            Repository.AddDeposit(deposit);

                            Window window = obj as Window;
                            window.Close();
                        }


                    },
                    obj => string.IsNullOrEmpty(ErrorMessage)));
            }
        }


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

                                if (result > Account.Balance)
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
