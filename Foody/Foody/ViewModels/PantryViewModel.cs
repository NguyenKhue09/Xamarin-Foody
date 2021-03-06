using Foody.Models;
using Foody.Views;
using Foody.Views.PopUp;
using MvvmHelpers;
using Rg.Plugins.Popup.Extensions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Foody.ViewModels
{
    public class PantryViewModel : BaseViewModel
    {
        public int SelectedTabIndex { get; set; }

        private SearchPopUp searchPopUp = new SearchPopUp();

        public List<string> cuisineList { get; set; }
        public List<string> intolerancesList { get; set; }

        public ObservableRangeCollection<Result> PantryRecipes { get; set; }
        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public ObservableRangeCollection<Result> RandomRecipes { get; set; }
        public ObservableRangeCollection<Result> SearchRecipes { get; set; }
        public Command NavToPantry { get; }
        public Command Test { get; }

        public Command ChipCuisineSelectedCommand { get; }
        public Command ChipCuisineUnselectedCommand { get; }

        public Command ChipIntolerancesSelectedCommand { get; }
        public Command ChipIntolerancesUnselectedCommand { get; }

        //
        public ObservableRangeCollection<PantryBuilder> SearchUserPantryItems { get; set; }

        public bool isShowSearchIngredientItem = false;

        public bool isShowSearchRecipe = false;

        public string showHeightResultSearch = "0,0,0,0";

        public string ShowHeightResultSearch
        {
            get { return showHeightResultSearch; }
            set { SetProperty(ref showHeightResultSearch, value); }
        }

        public bool IsShowSearchIngredientItem
        {
            get { return isShowSearchIngredientItem; }
            set { SetProperty(ref isShowSearchIngredientItem, value); }
        }

        public bool IsShowSearchRecipe
        {
            get { return isShowSearchRecipe; }
            set { SetProperty(ref isShowSearchRecipe, value); }
        }

        public Command Checkmanager { get; }


        INavigation Navigation;

        public ObservableCollection<UserPantryListGroupManager> UserPantryListGroupManagers { get; set; }
        private ObservableCollection<ItemId> originalUserPantryItems { get; set; }

        private ObservableCollection<ItemId> selectedUserPantryItems { get; set; }
        private ObservableCollection<string> selectedUserPantryItemsId { get; set; }

        public PantryViewModel(INavigation MainPageNav)
        {
            Navigation = MainPageNav;
            PantryRecipes = new ObservableRangeCollection<Result>();
            PopularRecipes = new ObservableRangeCollection<Result>();
            RandomRecipes = new ObservableRangeCollection<Result>();
            SearchRecipes = new ObservableRangeCollection<Result>();
            NavToPantry = new Command(async () => await OpenPagePantrySetting(), () => !IsBusy);
            Test = new Command(async () => await showpopup_Clicked(), () => !IsBusy);
            ChipCuisineSelectedCommand = new Command<string>(chipCuisineSelected);
            ChipCuisineUnselectedCommand = new Command<string>(chipCuisineUnSelected);
            ChipIntolerancesSelectedCommand = new Command<string>(chipIntolerancesSelected);
            ChipIntolerancesUnselectedCommand = new Command<string>(chipIntolerancesUnSelected);
            intolerancesList = new List<string>();
            cuisineList = new List<string>();
            SearchUserPantryItems = new ObservableRangeCollection<PantryBuilder>();
            //
            Checkmanager = new Command<string>(manager_SelectionChanged);

            // pantry
            UserPantryListGroupManagers = new ObservableCollection<UserPantryListGroupManager>();
            originalUserPantryItems = new ObservableCollection<ItemId>();
            selectedUserPantryItems = new ObservableCollection<ItemId>();
            selectedUserPantryItemsId = new ObservableCollection<string>();
        }

        public async Task OpenPagePantrySetting()
        {
            await Navigation.PushAsync(new PagePantrySetting());
        }


        async public void GetPantryRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetPantryRecipes();
            PantryRecipes.AddRange(results.results);
        }
        async public void GetPopularRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetPopularRecipes();
            PopularRecipes.AddRange(results.results);
        }

        async public void GetRandomRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetRandomRecipes();
            RandomRecipes.AddRange(results.results);

        }

        async public void GetSearchRecipes(string query, string cuisine, string intolerances)
        {
            Recipe results = new Recipe();
            if(!IsShowSearchRecipe)
            {
                IsShowSearchRecipe = true;
                await showpopup_Clicked();
                results = await App.RecipeManager.SearchRecipes(query, cuisine, intolerances);
                SearchRecipes.AddRange(results.results);
                if (results != null && results.results.Count > 0)
                {
                    IsShowSearchRecipe = false;
                    await searchPopUp.closeSearchPopup();
                    await Navigation.PushAsync(new PageSearchRecipes(results));
                }
                else
                {
                    IsShowSearchRecipe = false;
                    await searchPopUp.closeSearchPopup();
                }
            }
            
        }

        async public Task showpopup_Clicked()
        {
            await Navigation.PushPopupAsync(new SearchPopUp());
        }


        public PantryViewModel()
        {
            SelectedTabIndex = 0;
        }


        public void chipCuisineSelected(string value)
        {
            if(!cuisineList.Contains(value))
            {
                cuisineList.Add(value);
            }
        }

        public void chipCuisineUnSelected(string value)
        {
            if(value != "" || value != null)
            {
                cuisineList.Remove(value);
            }
        }

        public void chipIntolerancesSelected(string value)
        {
            if (!intolerancesList.Contains(value))
            {
                intolerancesList.Add(value);
            }
        }

        public void chipIntolerancesUnSelected(string value)
        {
            if (value != "" || value != null)
            {
                intolerancesList.Remove(value);
            }

        }
        //
        public void manager_SelectionChanged(string topic)
        {
            changeExpand(topic);
        }
        public void changeExpand(string item)
        {
            foreach (UserPantryListGroupManager group in UserPantryListGroupManagers)
            {

                if (item == group.Aisle)
                {
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }

        //search pantry manager
        async public void SearchUserPantryItem(string searchString)
        {
            PantryBuilderResult results = await App.RecipeManager.SearchPantryBuilder(searchString);

            if (results != null)
            {
                if (results.pantryBuilder.Count > 0 && searchString != "")
                {

                    SearchUserPantryItems.Clear();
                    SearchUserPantryItems.AddRange(results.pantryBuilder);
                    if (results.pantryBuilder.Count == 1)
                    {
                        ShowHeightResultSearch = "0,0,280,65";
                    }
                    else if (results.pantryBuilder.Count == 2)
                    {
                        ShowHeightResultSearch = "0,0,280,125";
                    }
                    else
                    {
                        ShowHeightResultSearch = "0,0,280,185";
                    }
                    IsShowSearchIngredientItem = true;
                }
                else
                {
                    IsShowSearchIngredientItem = false;
                }
            }

        }


        // Pantry
        public async Task<ObservableCollection<UserPantryListGroupManager>> GetOriginalPantryBuilderItems()
        {
            UserPantryItemResult userPantryItemResult = await App.RecipeManager.GetUserPantryItems(App.LoginViewModel.GoogleUser.UID);
            ObservableCollection < UserPantryListGroupManager > userPantryListGroups = new ObservableCollection<UserPantryListGroupManager>();
            if (userPantryItemResult != null)
            {
                originalUserPantryItems = new ObservableRangeCollection<ItemId>(userPantryItemResult.pantryItems.itemId);
            } else
            {
                originalUserPantryItems.Clear();
            }
            var queryPantryBuilderAisle = from item in originalUserPantryItems
                                          group item by item.aisle into newResults
                                          orderby newResults.Key
                                          select newResults;
            foreach (var aisleGroup in queryPantryBuilderAisle)
            {
                userPantryListGroups.Add(new UserPantryListGroupManager(
                    aisleGroup.Key,
                    new ObservableCollection<ItemId>(aisleGroup.ToList())
                ));
            }
            return userPantryListGroups;
        }

        public void GetSelectedUserPantryItem()
        {
            foreach (UserPantryListGroupManager group in UserPantryListGroupManagers)
            {
                foreach (ItemId item in group.UserPantryListItems)
                {
                    if (item.IsChoose && !selectedUserPantryItems.Contains(item))
                    {
                        selectedUserPantryItems.Add(item);
                        selectedUserPantryItemsId.Add(item._id);
                    }
                    else if (!item.IsChoose && selectedUserPantryItems.Contains(item))
                    {
                        selectedUserPantryItems.Remove(item);
                        selectedUserPantryItemsId.Remove(item._id);
                    }
                }
            }
        }

        public async Task<bool> AddItemUserPantry(UserPantryItem userPantryItem)
        {
            bool result = await App.RecipeManager.AddItemToUserPantry(userPantryItem);
            return result;
        }
        public async Task<int> DeleteSelectedUserPantryItem()
        {
            GetSelectedUserPantryItem();

            bool result = await App.RecipeManager.DeleteManyUserPantryItem(selectedUserPantryItemsId.ToList());
            if(result)
            {
                foreach (ItemId itemId in selectedUserPantryItems)
                {
                    foreach (UserPantryListGroupManager userPantryListGroupManager in UserPantryListGroupManagers)
                    {
                        if (itemId.aisle == userPantryListGroupManager.Aisle)
                        {
                            userPantryListGroupManager.UserPantryListItems.Remove(itemId);
                            if (userPantryListGroupManager.UserPantryListItems.Count == 0)
                            {
                                deleteUserPantryGroupManagerItem(userPantryListGroupManager);
                            }
                            break;
                        }

                    }
                }
            }

            selectedUserPantryItems.Clear();
            selectedUserPantryItemsId.Clear();

            return UserPantryListGroupManagers.Count;
        }

        public async Task<bool> DeleteAllUserPantryItem()
        {
            bool result = await App.RecipeManager.DeleteAllUserPantryItem(App.LoginViewModel.GoogleUser.UID);
            if(result)
            {
                UserPantryListGroupManagers.Clear();
            } 
            return result;
        }

        public async Task<bool> DeleteUserPantryItem(ItemId itemId)
        {
            bool result = false;

            foreach (ItemId item in originalUserPantryItems)
            {
                if (item._id == itemId._id)
                {
                    result = await App.RecipeManager.DeleteUserPantryItem(item._id, App.LoginViewModel.GoogleUser.UID);
                }
            }


            if (result)
            {
                foreach (UserPantryListGroupManager userPantryListGroupManager in UserPantryListGroupManagers)
                {
                    if (itemId.aisle == userPantryListGroupManager.Aisle)
                    {
                        userPantryListGroupManager.UserPantryListItems.Remove(itemId);
                        if (userPantryListGroupManager.UserPantryListItems.Count == 0)
                        {
                            deleteUserPantryGroupManagerItem(userPantryListGroupManager);
                        }
                        break;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        void deleteUserPantryGroupManagerItem(UserPantryListGroupManager userPantryListGroupManager)
        {
            UserPantryListGroupManagers.Remove(userPantryListGroupManager);
        }

    } 
    
}
