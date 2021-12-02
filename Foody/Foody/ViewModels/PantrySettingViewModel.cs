using Foody.Data.Local;
using Foody.Models;
using Foody.Models.Local;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Foody.ViewModels
{
    public class PantrySettingViewModel : BaseViewModel
    {
        public Command Checkmanager { get; }

        public ObservableCollection<PantryBuilderListGroupManager> PantryBuilderListGroupManagers { get; set; }

        private List<PantryBuilderItem> originalPantryBuilderItems { get; set; }

        public PantrySettingViewModel()
        {
            PantryBuilderListGroupManagers = new ObservableCollection<PantryBuilderListGroupManager>();
            originalPantryBuilderItems = new PantryBuilder().pantryBuilderItems;
            Checkmanager = new Command<string>(changeExpand);
        }

        public void GetPantryBuilderList()
        {

            var queryPantryBuilderAisle = from item in originalPantryBuilderItems
                                          group item by item.aisleBelong into newResults
                                          orderby newResults.Key
                                          select newResults;

            
            foreach (var aisleGroup in queryPantryBuilderAisle)
            {
                PantryBuilderListGroupManagers.Add(new PantryBuilderListGroupManager(
                    aisleGroup.Key,
                    new ObservableCollection<PantryBuilderItem>(aisleGroup.ToList())
                ));
            }

        }

        public void changeExpand(string item)
        {
            foreach (PantryBuilderListGroupManager group in PantryBuilderListGroupManagers)
            {
                
                if (item == group.Aisle)
                {
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }
    }
}
