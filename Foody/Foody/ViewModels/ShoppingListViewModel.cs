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
        public ObservableCollection<ShoppingListGroupManager> shoppingCartGroupAisleBelong { get; set; }

        public ObservableRangeCollection<IngredientInform> SearchIngredients { get; set; }
        private ShoppingListResult originalShoppintLists { get; set; }

        public bool isSelectedAllShoppingListItem = false;
        public bool isShowSearchIngredientItem = false;

        // group aisle shopping cart
        IOrderedEnumerable<IGrouping<string, CartIngredient>> queryIngredientAisle;

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
            Checkmanager = new Command<string>(changeExpand);
            shoppingListGroupManagers = new ObservableCollection<ShoppingListGroupManager>();
            CheckGroupAisleBelong = new Command<string>(changeExpandIcon);
            shoppingCartGroupAisleBelong = new ObservableCollection<ShoppingListGroupManager>();
            SearchIngredients = new ObservableRangeCollection<IngredientInform>();
            selectedShoppingtListItems = new ObservableCollection<ShoppingListItem>();
        }

        
        public void changeExpand(string item)
        {
            foreach (ShoppingListGroupManager group in shoppingListGroupManagers)
            {
                if (item == group.Aisle)
                {
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

            var queryIngredientAisle = from item in originalShoppintLists.results
                                       group item by item.aisle into newResults
                                       orderby newResults.Key
                                       select newResults;

            foreach(var nameGroup in queryIngredientAisle)
            {
                
                ShoppingListGroupManager shoppingListGroupManager = new ShoppingListGroupManager(nameGroup.Key, await SumOfShoppingListItemFromApi(nameGroup));
                shoppingListGroupManagers.Add(shoppingListGroupManager);
            }

        }

        Task<ObservableCollection<ShoppingListItem>> SumOfShoppingListItemFromApi(IGrouping<string, Item> nameGroup)
        {
            double amount = 0;
            ObservableCollection<ShoppingListItem> shoppingListItems = new ObservableCollection<ShoppingListItem>();

           
            
            var queryIngredientId = from item in nameGroup.ToList()
                                    group item by item.id into newResults
                                    orderby newResults.Key
                                    select newResults;
            foreach (var group in queryIngredientId)
            {
                ShoppingListItem shoppingListItem = new ShoppingListItem();
                foreach (var item in group)
                {
                    amount += item.amount;
                    shoppingListItem.IngredientName = item.name;
                    shoppingListItem.IngredientAisle = item.aisle;
                    shoppingListItem.IngredientIdList = item._id;
                    shoppingListItem.IngredientId = item.id;
                    shoppingListItem.IngredientUnits = item.unit;
                    shoppingListItem.IngredientImg = item.image;
                }
                shoppingListItem.StringIngredientAmount = new Fraction(Math.Round(amount, 2)).ToString();
                shoppingListItem.IngredientAmount = amount;
                amount = 0;
                shoppingListItem.IsChoose = false;
                shoppingListItems.Add(shoppingListItem);
            }
            return Task.FromResult(shoppingListItems);
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
            //Debug.WriteLine(selectedShoppingtListItems.Count);

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

            //Debug.WriteLine(IsSelectedAllShoppingListItem);
            
            return true;
        }

        public async Task<bool> DeleteShoppingListItem(ShoppingListItem shoppingListItem)
        {
            bool result = false;

            foreach (Item item in originalShoppintLists.results)
            {
                if (item.id == shoppingListItem.IngredientId)
                {
                    result = await App.RecipeManager.DeleteShoppingListItem(item._id);
                }
            }


            if (result)
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
                if (results.results.Count > 0 && searchString != "")
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
       
        public void changeExpandIcon(string item)
        {
            foreach (ShoppingListGroupManager group in shoppingCartGroupAisleBelong)
            {


                if (item == group.Aisle)
                {
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                }
            }
        }

        //Shopping cart data 
        async public void GetShoppingCart()
        {
            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;
            List<CartIngredient> cartIngredients = await recipeDatabase.GetIngredientAsync(App.LoginViewModel.ObsGoogleUser.UID);
            queryIngredientAisle = from item in cartIngredients
                                       group item by item.aisleBelong into newResults
                                       orderby newResults.Key
                                       select newResults;
           
            foreach (var namegroup in queryIngredientAisle)
            {
                var queryIngredientId = from item in namegroup
                                        group item by item.ingredientId into newResults
                                        orderby newResults.Key
                                        select newResults;
                ShoppingListGroupManager shoppingCartGroupManager = new ShoppingListGroupManager(namegroup.Key, await SumOfShoppingCartItemFromApi(queryIngredientId));
                shoppingCartGroupAisleBelong.Add(shoppingCartGroupManager);
            }
        }

        async Task<ObservableCollection<ShoppingListItem>> SumOfShoppingCartItemFromApi(IOrderedEnumerable<IGrouping<int, CartIngredient>> queryIngredientId)
        {
            double amount = 0;
            ObservableCollection<ShoppingListItem> shoppingCartItems = new ObservableCollection<ShoppingListItem>();

            foreach (var nameGroup in queryIngredientId)
            {
                ShoppingListItem shoppingCartItem = new ShoppingListItem();
                foreach (var item in nameGroup)
                {
                    amount += item.amount;
                    shoppingCartItem.IngredientName = item.ingredientName;
                    shoppingCartItem.IngredientAisle = item.aisleBelong;
                    //shoppingCartItem.IngredientIdList = item.ingredientId;
                    shoppingCartItem.IngredientUnits = item.ingredientUnits;
                }
                shoppingCartItem.StringIngredientAmount = new Fraction(Math.Round(amount, 2)).ToString();
                shoppingCartItem.IngredientAmount = amount;
                shoppingCartItem.IngredientId = nameGroup.Key;
                shoppingCartItem.IsChoose = true;
                IngredientInform ingredientInform = await GetIngredientInform(nameGroup.Key);
                if (ingredientInform != null)
                {
                    shoppingCartItem.IngredientImg = ingredientInform.image;
                }
                shoppingCartItems.Add(shoppingCartItem);
                amount = 0;


            }
            return shoppingCartItems;
        }

        //Delete shopping cart item
        void deleteShoppingCartGroupManagerItem(ShoppingListGroupManager shoppingCartGroupManager)
        {
            shoppingCartGroupAisleBelong.Remove(shoppingCartGroupManager);
        }
        public async Task<bool>DeleteShoppingCartItem(ShoppingListItem shoppingListItem)
        {
            int result = 0;
            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;
            List<CartIngredient> cartIngredients = await recipeDatabase.GetIngredientAsync(App.LoginViewModel.ObsGoogleUser.UID);
            foreach (var aisle in queryIngredientAisle)
            {
                if (aisle.Key == shoppingListItem.IngredientAisle)
                {
                    foreach (CartIngredient cartIngredient in cartIngredients)
                    {
                        if (cartIngredient.ingredientId == shoppingListItem.IngredientId)
                        {
                            result = await recipeDatabase.DeleteIngredientAsync(cartIngredient);
                        }
                    }
                }
            }


            if (result != 0)
            {
                foreach (ShoppingListGroupManager shoppingCartGroupManager in shoppingCartGroupAisleBelong)
                {
                    if (shoppingListItem.IngredientAisle == shoppingCartGroupManager.Aisle)
                    {
                        shoppingCartGroupManager.ShoppingListItems.Remove(shoppingListItem);
                        if (shoppingCartGroupManager.ShoppingListItems.Count == 0)
                        {
                            Debug.WriteLine("Empty list");
                            deleteShoppingCartGroupManagerItem(shoppingCartGroupManager);
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
    }
}
