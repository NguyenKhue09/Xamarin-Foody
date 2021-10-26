using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Foody.Models
{
    // data gia
    public class Speaker
    {
        public string Name { get; set; }
        public string image { get; set; }
    }
    public class GroupManage : ObservableCollection<Speaker>, INotifyPropertyChanged
    {

        private bool _expanded;

        public string Title { get; set; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }

        public string StateIcon
        {
            get { return Expanded ? "up.png" : "down.png"; }
        }
        public GroupManage(string title,  bool expanded = false)
        {
            Title = title;
            Expanded = expanded;
        }

        public int FoodCount { get; set; }
        public static ObservableCollection<GroupManage> All { private set; get; }
        
        static GroupManage()
        {
            ObservableCollection<GroupManage> Groups = new ObservableCollection<GroupManage>{
                new GroupManage("Carbohydrates"){
                    new Speaker { Name = "Maddy Leger", image = "anh1" }, 
                    new Speaker { Name = "David Ortinau", image = "anh2" }, 
                    new Speaker { Name = "Gerald Versluis", image = "anh3" }
                },
                new GroupManage("Fruits"){
                   new Speaker { Name = "Maddy Leger", image = "07:30" }, 
                   new Speaker { Name = "David Ortinau", image = "08:30" }, 
                   new Speaker { Name = "Gerald Versluis", image = "10:30" }
                },
                new GroupManage("Vegetables"){
                    new Speaker { Name = "Maddy Leger", image = "anh1" },
                    new Speaker { Name = "David Ortinau", image = "anh2" },
                    new Speaker { Name = "Gerald Versluis", image = "anh3" }
                },
                new GroupManage("Dairy"){
                    new Speaker { Name = "Maddy Leger", image = "07:30" },
                   new Speaker { Name = "David Ortinau", image = "08:30" },
                   new Speaker { Name = "Gerald Versluis", image = "10:30" }

                } };
            All = Groups;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}