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
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class PantrySettingViewModel : BaseViewModel
    {
        public Xamarin.Forms.Command Checkmanager { get; }

        public ObservableCollection<PantryBuilderListGroupManager> PantryBuilderListGroupManagers { get; set; }

        private ObservableRangeCollection<PantryBuilder> originalPantryBuilderItems { get; set; }

        public PantrySettingViewModel()
        {
            PantryBuilderListGroupManagers = new ObservableCollection<PantryBuilderListGroupManager>();
            originalPantryBuilderItems = new ObservableRangeCollection<PantryBuilder>();
            Checkmanager = new Xamarin.Forms.Command<string>(changeExpand);
        }

        public async void GetOriginalPantryBuilderItems()
        {
            PantryBuilderResult pantryBuilderResult = await App.RecipeManager.GetPantrybuilderList();
            if(pantryBuilderResult != null)
            {
                originalPantryBuilderItems  = new ObservableRangeCollection<PantryBuilder>(pantryBuilderResult.pantryBuilder);
            }
            GetPantryBuilderList();
        }

        public void GetPantryBuilderList()
        {
            
            var queryPantryBuilderAisle = from item in originalPantryBuilderItems
                                          group item by item.aisle into newResults
                                          orderby newResults.Key
                                          select newResults;
            
            foreach (var aisleGroup in queryPantryBuilderAisle)
            {
                PantryBuilderListGroupManagers.Add(new PantryBuilderListGroupManager(
                    aisleGroup.Key,
                    new ObservableCollection<PantryBuilder>(aisleGroup.ToList())
                ));
            }

        }

        public async void SearchOriginalPantryBuilderItems(string searchString)
        {
           
            PantryBuilderResult pantryBuilderResult = await App.RecipeManager.SearchPantryBuilder(searchString);
            if (pantryBuilderResult != null)
            {
                PantryBuilderListGroupManagers.Clear();
                originalPantryBuilderItems.Clear();
                originalPantryBuilderItems.AddRange(pantryBuilderResult.pantryBuilder);
            }
            GetPantryBuilderList();
        }

        public async void GetSelectedPantryBuidlerItem()
        {
            foreach (PantryBuilderListGroupManager group in PantryBuilderListGroupManagers)
            {

                foreach(PantryBuilder item in group.PantryBuilderListItems)
                {
                    if(item.IsChoose)
                    {
                        UserPantryItem userPantryItem = new UserPantryItem
                        {
                            userId = App.LoginViewModel.GoogleUser.UID,
                            itemId = item._id
                        };
                        _ = await App.RecipeManager.AddItemToUserPantry(userPantryItem);
                    }
                   
                }
            }
             await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/pantry", true);
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
