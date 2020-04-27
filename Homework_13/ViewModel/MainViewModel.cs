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

        private Group selectedGroup;
        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                ClientsInGroup = db.Clients.Where(x => x.GroupId == SelectedGroup.Id).ToList();
                OnPropertyChanged("SelectedGroup");
            }
        }


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


        private IEnumerable<Client> clientsInGroup;
        public IEnumerable<Client> ClientsInGroup
        {
            get { return clientsInGroup; }
            set
            {
                clientsInGroup = value;
                OnPropertyChanged("ClientsInGroup");
            }
        }



        #region Commands

        private RelayCommand addGroup;

        public RelayCommand AddGroup
        {
            get
            {
                return addGroup ??
                    (addGroup = new RelayCommand(obj =>
                    {
                        Group group = new Group()
                        {
                            Name = "АвтоСоздание"
                        };

                        
                        db.Groups.Add(group);
                        db.SaveChanges();

                        Groups = db.Groups.ToList();

                    }));
            }
        }

        #endregion


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
