using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_13.Model;

namespace Homework_13.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        HW13Context db;


        private IEnumerable<Group> groups;
        public IEnumerable<Group> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }


        public MainViewModel()
        {
            db = new HW13Context();
            Groups = db.Groups.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
