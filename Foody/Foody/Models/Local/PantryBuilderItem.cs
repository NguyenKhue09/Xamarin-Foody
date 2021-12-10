//using SQLite;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Text;

//namespace Foody.Models.Local
//{
//    public class PantryBuilderItem: INotifyPropertyChanged
//    {
//        public string aisle { get; set; }

//        public string image { get; set; }

//        public int id { get; set; }

//        public string name { get; set; }

//        public bool isChoose = false;

//        public bool IsChoose
//        {
//            get { return isChoose; }
//            set
//            {
//                if (isChoose != value)
//                {
//                    isChoose = value;
//                    OnPropertyChanged("IsChoose");
//                }
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }


//}
