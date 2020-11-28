using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13.ViewModel
{
    class NotificationWindowViewModel : BaseViewModel
    {
        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
    }
}
