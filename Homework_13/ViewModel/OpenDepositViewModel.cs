using Homework_13.Helper;
using Homework_13.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace Homework_13.ViewModel
{
    class OpenDepositViewModel : BaseViewModel
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
                        if (decimal.TryParse(Amount, out decimal result))
                        {
                            if (result > Account.Balance)
                            {
                                ErrorMessage = "Недостаточно средств";
                                return;
                            }

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
                    obj => {
                        if (string.IsNullOrEmpty(Amount))
                        {
                            ErrorMessage = "";
                            return false;
                        }

                        if(decimal.TryParse(Amount, out decimal result))
                        {
                            ErrorMessage = "";
                            return true;
                        }


                        ErrorMessage = "введены недопустимые символы";
                        return false;
                    }));
            }
        }


    }
}
