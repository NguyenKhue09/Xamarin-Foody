using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Foody.Models
{
    public class Speaker
    {
        public string Name { get; set; }
        public string Time { get; set; }
    }
    public class GroupManager : INotifyPropertyChanged
    {
        public string Topic { get; set; }

        public bool isExpanded;

        public string iconExpand;

        public ObservableCollection<Speaker> speakers { get; set; }

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

        public GroupManager(string topic, ObservableCollection<Speaker> speaker , string iconExpand = "down.png", bool isExpanded = true)
        {
            Topic = topic;
            speakers = new ObservableCollection<Speaker>( speaker);
            IconExpand = iconExpand;
            IsExpanded = new bool();
            IsExpanded = isExpanded;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
