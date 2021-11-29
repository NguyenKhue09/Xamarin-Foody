using Foody.Converters;
using Foody.Data.Local;
using Foody.Models;
using Foody.Models.Local;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Foody.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        public Command Checkmanager { get; }
        public Command CheckGroupAisleBelong { get; }

        public ObservableCollection<ShoppingListGroupManager> shoppingListGroupManagers { get; set; }
        public ObservableCollection<ShoppingListGroupManager> shoppingListGroupAisleBelong { get; set; }

        public ObservableRangeCollection<IngredientInform> SearchIngredients { get; set; }
        private ShoppingListResult originalShoppintLists { get; set; }

        public bool isSelectedAllShoppingListItem = false;
        public bool isShowSearchIngredientItem = false;

        public string showHeightResultSearch = "0,0,0,0";

        public string ShowHeightResultSearch {
            get { return showHeightResultSearch; }
            set { SetProperty(ref showHeightResultSearch, value); }
        }

        public bool IsSelectedAllShoppingListItem
        {
            get { return isSelectedAllShoppingListItem; }
            set { SetProperty(ref isSelectedAllShoppingListItem, value); }
        }

        public bool IsShowSearchIngredientItem
        {
            get { return isShowSearchIngredientItem; }
            set { SetProperty(ref isShowSearchIngredientItem, value); }
        }

        public ObservableCollection<ShoppingListItem> selectedShoppingtListItems { get; set; }

        public ShoppingListViewModel()
        {
            Checkmanager = new Command<string>(manager_SelectionChanged);
            shoppingListGroupManagers = new ObservableCollection<ShoppingListGroupManager>();
            CheckGroupAisleBelong = new Command<string>(aisleBelong_SelectionChanged);
            shoppingListGroupAisleBelong = new ObservableCollection<ShoppingListGroupManager>();
            SearchIngredients = new ObservableRangeCollection<IngredientInform>();
            selectedShoppingtListItems = new ObservableCollection<ShoppingListItem>();
        }

        public void manager_SelectionChanged(string topic)
        {
            changeExpand(topic);
        }



        public void changeExpand(string item)
        {
            foreach (ShoppingListGroupManager group in shoppingListGroupManagers)
            {


                if (item == group.Aisle)
                {
                    group.IsExpanded = !group.IsExpanded;
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }


        // Shopping list data
        async public void GetShoppingList()
        {
            originalShoppintLists = new ShoppingListResult();
            //shoppingListGroupManagers = new ObservableCollection<ShoppingListGroupManager>();

            originalShoppintLists = await App.RecipeManager.GetShoppingList();

            foreach (Aisle aisle in originalShoppintLists.aisles)
            {
                var queryIngredientId = from item in aisle.items
                                        group item by item.ingredientId into newResults
                                        orderby newResults.Key
                                        select newResults;
                ShoppingListGroupManager shoppingListGroupManager = new ShoppingListGroupManager(aisle.aisle, await SumOfShoppingListItemFromApi(queryIngredientId));
                shoppingListGroupManagers.Add(shoppingListGroupManager);
            }
           
        }

        async Task<ObservableCollection<ShoppingListItem>> SumOfShoppingListItemFromApi(IOrderedEnumerable<IGrouping<int, Item>> queryIngredientId)
        {
            double amount = 0;
            ObservableCollection<ShoppingListItem> shoppingListItems = new ObservableCollection<ShoppingListItem>();

            foreach (var nameGroup in queryIngredientId)
            {
                ShoppingListItem shoppingListItem = new ShoppingListItem();
                foreach (var item in nameGroup)
                {
                    amount += item.measures.original.amount;
                    shoppingListItem.IngredientName = item.name;
                    shoppingListItem.IngredientAisle = item.aisle;
                    shoppingListItem.IngredientIdList = item.id;
                    shoppingListItem.IngredientUnits = item.measures.original.unit;
                }
                shoppingListItem.StringIngredientAmount = new Fraction(Math.Round(amount, 2)).ToString();
                shoppingListItem.IngredientAmount = amount;
                shoppingListItem.IngredientId = nameGroup.Key;
                shoppingListItem.IsChoose = false;
                IngredientInform ingredientInform = await GetIngredientInform(nameGroup.Key);
                if (ingredientInform != null)
                {
                    shoppingListItem.IngredientImg = ingredientInform.image;
                }
                shoppingListItems.Add(shoppingListItem);
                amount = 0;


            }
            
            return shoppingListItems;
        }

        // add to cart
        public void GetSelectedShoppingListItem()
        {
            foreach (ShoppingListGroupManager shoppingListGroupManager in shoppingListGroupManagers)
            {
                foreach(ShoppingListItem shoppingListItem in shoppingListGroupManager.shoppingListItems)
                {

                    if(shoppingListItem.isChoose && !selectedShoppingtListItems.Contains(shoppingListItem))
                    {
                        selectedShoppingtListItems.Add(shoppingListItem);
                    } else if(!shoppingListItem.isChoose && selectedShoppingtListItems.Contains(shoppingListItem))
                    {
                        selectedShoppingtListItems.Remove(shoppingListItem);
                    }
                    
                    Debug.WriteLine(shoppingListItem.IngredientName + shoppingListItem.isChoose);
                }
            }
        }

        public void SelectAllShoppingListItem()
        {
            foreach (ShoppingListGroupManager shoppingListGroupManager in shoppingListGroupManagers)
            {
                foreach (ShoppingListItem shoppingListItem in shoppingListGroupManager.ShoppingListItems)
                {
                    
                        shoppingListItem.IsChoose = isSelectedAllShoppingListItem;
                    //Debug.WriteLine(shoppingListItem.IngredientName + shoppingListItem.IsChoose);
                }
            }
        }

        public async Task<bool> DeleteAllSelectedShoppingListItem()
        {

            GetSelectedShoppingListItem();
            Debug.WriteLine(selectedShoppingtListItems.Count);

            foreach (ShoppingListItem shoppingListItem in selectedShoppingtListItems)
            {

                bool result = await DeleteShoppingListItem(shoppingListItem);
                if (!result)
                {
                    return false;
                }
            }

            if(IsSelectedAllShoppingListItem)
            {
                IsSelectedAllShoppingListItem = false;
            }

            Debug.WriteLine(IsSelectedAllShoppingListItem);
            
            return true;
        }

        public async Task<bool> DeleteShoppingListItem(ShoppingListItem shoppingListItem)
        {
            bool result = false;

            foreach (Aisle aisle in originalShoppintLists.aisles)
            {
                if(aisle.aisle == shoppingListItem.IngredientAisle)
                {
                    foreach(Item item in aisle.items)
                    {
                        if (item.name == shoppingListItem.IngredientName)
                        {
                            result = await App.RecipeManager.DeleteShoppingListItem(item.id);
                        }
                    }
                }
            }

            
            if(result)
            {
                foreach(ShoppingListGroupManager shoppingListGroupManager in shoppingListGroupManagers)
                {
                    if(shoppingListItem.IngredientAisle == shoppingListGroupManager.Aisle)
                    {
                        shoppingListGroupManager.ShoppingListItems.Remove(shoppingListItem);
                        if (shoppingListGroupManager.ShoppingListItems.Count == 0)
                        {
                            Debug.WriteLine("Empty list");
                            deleteShoppingListGroupManagerItem(shoppingListGroupManager);
                        }
                        break;
                    }
                    
                }
                return true;
            } else
            {
                return false;
            }
        }

        void deleteShoppingListGroupManagerItem(ShoppingListGroupManager shoppingListGroupManager)
        {
            shoppingListGroupManagers.Remove(shoppingListGroupManager);
        }

        async Task<IngredientInform> GetIngredientInform(int id)
        {
            return await App.RecipeManager.GetIngredientInform(id);
        }


        async public void SearchIngredient(string searchString)
        {
            
            SearchIngredientsResult results = await App.RecipeManager.SearchIngredients(searchString);

            if (results != null)
            {
                if (results.results.Count > 0)
                {
                       
                    SearchIngredients.Clear();
                    SearchIngredients.AddRange(results.results);
                    if (results.results.Count == 1)
                    {
                        ShowHeightResultSearch = "0,0,280,65";
                    }
                    else if (results.results.Count == 2)
                    {
                        ShowHeightResultSearch = "0,0,280,125";
                    }
                    else
                    {
                        ShowHeightResultSearch = "0,0,280,185";
                    }
                    //Debug.WriteLine(ShowHeightResultSearch);
                    IsShowSearchIngredientItem = true;
                }
                else
                {
                    IsShowSearchIngredientItem = false;
                }
            }
            
        }

        //protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}




        //Shopping cart 
        public void aisleBelong_SelectionChanged(string aisle)
        {
            changeExpandIcon(aisle);
        }
        public void changeExpandIcon(string item)
        {
            foreach (ShoppingListGroupManager group in shoppingListGroupAisleBelong)
            {


                if (item == group.Aisle)
                {
                    group.IsExpanded = !group.IsExpanded;
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }

        //Shopping cart data
        async public void GetShoppingCart()
        {
            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;
            List<CartIngredient> cartIngredients = await recipeDatabase.GetIngredientAsync(App.LoginViewModel.ObsGoogleUser.UID);
            var queryIngredientAisle = from item in cartIngredients
                                       group item by item.aisleBelong into newResults
                                       orderby newResults.Key
                                       select newResults;
           
            foreach (var namegroup in queryIngredientAisle)
            {
                var queryIngredientId = from item in namegroup
                                        group item by item.ingredientId into newResults
                                        orderby newResults.Key
                                        select newResults;
                ShoppingListGroupManager shoppingListGroupManager = new ShoppingListGroupManager(namegroup.Key, await SumOfShoppingCartItemFromApi(queryIngredientId));
                shoppingListGroupAisleBelong.Add(shoppingListGroupManager);
            }
        }

        async Task<ObservableCollection<ShoppingListItem>> SumOfShoppingCartItemFromApi(IOrderedEnumerable<IGrouping<int, CartIngredient>> queryIngredientId)
        {
            double amount = 0;
            ObservableCollection<ShoppingListItem> shoppingListItems = new ObservableCollection<ShoppingListItem>();

            foreach (var nameGroup in queryIngredientId)
            {
                ShoppingListItem shoppingListItem = new ShoppingListItem();
                foreach (var item in nameGroup)
                {
                    amount += item.amount;
                    shoppingListItem.IngredientName = item.ingredientName;
                    shoppingListItem.IngredientAisle = item.aisleBelong;
                    shoppingListItem.IngredientIdList = item.ingredientId;
                    shoppingListItem.IngredientUnits = item.ingredientUnits;
                }
                shoppingListItem.StringIngredientAmount = new Fraction(Math.Round(amount, 2)).ToString();
                shoppingListItem.IngredientAmount = amount;
                shoppingListItem.IngredientId = nameGroup.Key;
                shoppingListItem.IsChoose = true;
                IngredientInform ingredientInform = await GetIngredientInform(nameGroup.Key);
                if (ingredientInform != null)
                {
                    shoppingListItem.IngredientImg = ingredientInform.image;
                }
                shoppingListItems.Add(shoppingListItem);
                amount = 0;


            }

            return shoppingListItems;
        }

        //Delete shopping cart item
        
    }
}
