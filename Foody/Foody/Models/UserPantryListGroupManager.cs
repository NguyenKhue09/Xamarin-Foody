using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Foody.Models
{
    public class UserPantryListGroupManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Aisle { get; set; }

        public bool isExpanded;

        public string iconExpand;

        public ObservableCollection<ItemId> userPantryListItems { get; set; }

        public ObservableCollection<ItemId> UserPantryListItems
        {
            get { return userPantryListItems; }
            set
            {
                userPantryListItems = value;
                NotifyPropertyChanged();
            }
        }


        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public string IconExpand
        {
            get { return iconExpand; }
            set
            {
                if (iconExpand != value)
                {
                    iconExpand = value;
                    OnPropertyChanged("IconExpand");
                }
            }
        }

        public UserPantryListGroupManager(string aisle, ObservableCollection<ItemId> listItems, string iconExpand = "down.png", bool isExpanded = false)
        {
            Aisle = aisle;
            UserPantryListItems = new ObservableCollection<ItemId>(listItems);
            IconExpand = iconExpand;
            IsExpanded = isExpanded;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
