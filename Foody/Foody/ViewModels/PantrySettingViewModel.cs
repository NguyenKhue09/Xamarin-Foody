using Foody.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Foody.ViewModels
{
    public class PantrySettingViewModel : BaseViewModel
    {
        public Command Checkmanager { get; }

        public ObservableCollection<ShoppingListGroupManager> Groups = new ObservableCollection<ShoppingListGroupManager>{
                new ShoppingListGroupManager("Carbohydrates",
                  new ObservableCollection<ShoppingListItem>{ new ShoppingListItem { IngredientName = "pasta", IngredientAisle = "Carb Snakes" },
                    new ShoppingListItem { IngredientName = "potato", IngredientAisle = "The King of all Carbs" },
                    new ShoppingListItem { IngredientName = "bread", IngredientAisle = "Soft & Gentle"},
                    new ShoppingListItem { IngredientName = "rice", IngredientAisle = "Tiny grains of goodness" },
                 }),
                new ShoppingListGroupManager("Fruits",
                   new ObservableCollection<ShoppingListItem>{ new ShoppingListItem { IngredientName = "apple", IngredientAisle = "Keep the Doctor away"},
                    new ShoppingListItem { IngredientName = "banana", IngredientAisle = "This fruit is appeeling"},
                    new ShoppingListItem { IngredientName = "pear", IngredientAisle = "Pear with me"},
                }),
                new ShoppingListGroupManager("Vegetables",
                   new ObservableCollection<ShoppingListItem>{ new ShoppingListItem { IngredientName = "carrot", IngredientAisle = "Sounds like parrot"},
                    new ShoppingListItem { IngredientName = "green bean", IngredientAisle = "The less popular cousin of the baked bean"},
                    new ShoppingListItem { IngredientName = "broccoli", IngredientAisle = "Tiny food trees"},
                    new ShoppingListItem { IngredientName = "peas", IngredientAisle = "Peas sir, can I have some more?"},
                }),
                new ShoppingListGroupManager("Dairy",
                  new ObservableCollection<ShoppingListItem>{  new ShoppingListItem { IngredientName = "Milk", IngredientAisle = "Molk"},
                    new ShoppingListItem { IngredientName = "Cheese", IngredientAisle = "Cheese + Potato = <3"},
                    new ShoppingListItem { IngredientName = "Ice Cream", IngredientAisle = "Because I couldn't find an icon for yoghurt"},

                })
        };
        public ObservableCollection<ShoppingListGroupManager> manager { get; set; }

        public PantrySettingViewModel()
        {
            Checkmanager = new Command<string>(manager_SelectionChanged);
            manager = new ObservableCollection<ShoppingListGroupManager>(Groups);
        }

        public void manager_SelectionChanged(string topic)
        {
            changeExpand(topic);
        }
        public void changeExpand(string item)
        {
            foreach (ShoppingListGroupManager group in manager)
            {


                if (item == group.Aisle)
                {
                    group.IsExpanded = !group.IsExpanded;
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }
    }
}
