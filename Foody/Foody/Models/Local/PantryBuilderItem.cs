using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Foody.Models.Local
{
    public class PantryBuilderItem: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string aisleBelong { get; set; }

        public string ingredientImg { get; set; }

        public int ingredientId { get; set; }

        public string ingredientName { get; set; }

        public bool isChoose { get; set; }

        public bool IsChoose
        {
            get { return isChoose; }
            set
            {
                if (isChoose != value)
                {
                    isChoose = value;
                    OnPropertyChanged("IsChoose");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
