using Foody.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Foody.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public Command Checkmanager { get; }

        public ObservableCollection<GroupManager> Groups = new ObservableCollection<GroupManager>{
                new GroupManager("Carbohydrates",
                  new ObservableCollection<Speaker>{ new Speaker { Name = "pasta", Time = "Carb Snakes" },
                    new Speaker { Name = "potato", Time = "The King of all Carbs" },
                    new Speaker { Name = "bread", Time = "Soft & Gentle"},
                    new Speaker { Name = "rice", Time = "Tiny grains of goodness" },
                 }),
                new GroupManager("Fruits",
                   new ObservableCollection<Speaker>{ new Speaker { Name = "apple", Time = "Keep the Doctor away"},
                    new Speaker { Name = "banana", Time = "This fruit is appeeling"},
                    new Speaker { Name = "pear", Time = "Pear with me"},
                }),
                new GroupManager("Vegetables",
                   new ObservableCollection<Speaker>{ new Speaker { Name = "carrot", Time = "Sounds like parrot"},
                    new Speaker { Name = "green bean", Time = "The less popular cousin of the baked bean"},
                    new Speaker { Name = "broccoli", Time = "Tiny food trees"},
                    new Speaker { Name = "peas", Time = "Peas sir, can I have some more?"},
                }),
                new GroupManager("Dairy",
                  new ObservableCollection<Speaker>{  new Speaker { Name = "Milk", Time = "Molk"},
                    new Speaker { Name = "Cheese", Time = "Cheese + Potato = <3"},
                    new Speaker { Name = "Ice Cream", Time = "Because I couldn't find an icon for yoghurt"},

                })
        };
        public ObservableCollection<GroupManager> manager { get; set; }

        public ShoppingListViewModel()
        {
            Checkmanager = new Command<string>(manager_SelectionChanged);
            manager = new ObservableCollection<GroupManager>(Groups);
        }

        public void manager_SelectionChanged(string topic)
        {
            changeExpand(topic);
        }
        public void changeExpand(string item)
        {
            foreach (GroupManager group in manager)
            {


                if (item == group.Topic)
                {
                    group.IsExpanded = !group.IsExpanded;
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }

    }
}
