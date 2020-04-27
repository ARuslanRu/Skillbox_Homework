using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Homework_13.Model
{
    class Client : INotifyPropertyChanged
    {

        #region Fields

        private string name;

        private string status;

        #endregion

        #region Properties

        public int Id { get; set; }

        public int GroupId { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
