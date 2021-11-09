using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Foody.Models
{
    public class ShoppingListItem : INotifyPropertyChanged
    {
        public string IngredientName { get; set; }
        public string IngredientAisle { get; set; }
        public string IngredientImg { get; set; }
        public int IngredientId { get; set; }
        public int IngredientIdList { get; set; }
        public double IngredientAmount { get; set; }
        public string StringIngredientAmount { get; set; }
        public string IngredientUnits { get; set; }

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
    public class ShoppingListGroupManager :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Aisle { get; set; }

        public bool isExpanded;

        public string iconExpand;

        public ObservableCollection<ShoppingListItem> shoppingListItems { get; set; }

        public ObservableCollection<ShoppingListItem> ShoppingListItems
        {
            get { return shoppingListItems; }
            set
            {
                shoppingListItems = value;
                NotifyPropertyChanged();
                //OnPropertyChanged("ShoppingListItems");
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

        public ShoppingListGroupManager(string aisle, ObservableCollection<ShoppingListItem> listItems, string iconExpand = "down.png", bool isExpanded = false)
        {
            Aisle = aisle;
            ShoppingListItems = new ObservableCollection<ShoppingListItem>(listItems);
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
