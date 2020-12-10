namespace Homework_17.ViewModel
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
