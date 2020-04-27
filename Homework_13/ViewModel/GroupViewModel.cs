using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Homework_13.Model;
using System.Windows;

namespace Homework_13.ViewModel
{
    class GroupViewModel : INotifyPropertyChanged
    {
        HW13Context db;

        private Group group;

        public string Name
        {
            get { return group.Name; }
            set
            {
                group.Name = value;
                OnPropertyChanged("Name");
            }
        }


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        Group group = new Group()
                        {
                            Name = this.Name
                        };

                        db.Groups.Add(group);
                        db.SaveChanges();

                        Window window = obj as Window;
                        window.Close();

                    }, 
                    obj => !string.IsNullOrEmpty(Name)));
            }
        }


        public GroupViewModel(Group group)
        {
            db = new HW13Context();
            if (group == null)
            {
                this.group = new Group() { Name = "" };
            }
            else
            {
                this.group = group;
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
