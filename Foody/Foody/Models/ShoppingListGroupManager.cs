using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Foody.Models
{
    public class ShoppingListItem
    {
        public string IngredientName { get; set; }
        public string IngredientAisle { get; set; }
        public string IngredientImg { get; set; }
        public int IngredientId { get; set; }
        public int IngredientIdList { get; set; }
        public double IngredientAmount { get; set; }
        public string StringIngredientAmount { get; set; }
        public string IngredientUnits { get; set; }
    }
    public class ShoppingListGroupManager :INotifyPropertyChanged
    {
        public string Aisle { get; set; }

        public bool isExpanded;

        public string iconExpand;

        public ObservableCollection<ShoppingListItem> shoppingListItems { get; set; }

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
            shoppingListItems = new ObservableCollection<ShoppingListItem>(listItems);
            IconExpand = iconExpand;
            IsExpanded = isExpanded;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
