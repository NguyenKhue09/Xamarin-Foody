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
         public int totalItemShoppingCart { get; set; }
        public ObservableRangeCollection<IngredientInform> SearchIngredients { get; set; }
        private ShoppingListResult originalShoppintLists { get; set; }
        private ShoppingCartResult originalShoppintCarts { get; set; }

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
            CheckShoppingCart();
            Checkmanager = new Command<string>(changeExpand);
            shoppingListGroupManagers = new ObservableCollection<ShoppingListGroupManager>();
            CheckGroupAisleBelong = new Command<string>(changeExpandIcon);
            shoppingCartGroupAisleBelong = new ObservableCollection<ShoppingListGroupManager>();
            SearchIngredients = new ObservableRangeCollection<IngredientInform>();
            selectedShoppingtListItems = new ObservableCollection<ShoppingListItem>();
        }

        public async void CheckShoppingCart()
        {
            ShoppingCartResult result = await App.RecipeManager.GetShoppingCart();
            totalItemShoppingCart = result.resultsCart.Count;
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

        async public void GetShoppingCart()
        {
            originalShoppintCarts = new ShoppingCartResult();

            originalShoppintCarts = await App.RecipeManager.GetShoppingCart();
            var queryIngredientAisle = from item in originalShoppintCarts.resultsCart
                                       group item by item.aisle into newResults
                                       orderby newResults.Key
                                       select newResults;

            foreach (var nameGroup in queryIngredientAisle)
            {

                ShoppingListGroupManager shoppingCartGroupManager = new ShoppingListGroupManager(nameGroup.Key, await SumOfShoppingCartItemFromApi(nameGroup));
                shoppingCartGroupAisleBelong.Add(shoppingCartGroupManager);

            }

        }

        Task<ObservableCollection<ShoppingListItem>> SumOfShoppingCartItemFromApi(IGrouping<string, ItemCart> nameGroup)
        {
            double amount = 0;
            ObservableCollection<ShoppingListItem> shoppingCartItems = new ObservableCollection<ShoppingListItem>();



            var queryIngredientId = from item in nameGroup.ToList()
                                    group item by item.id into newResults
                                    orderby newResults.Key
                                    select newResults;
            foreach (var group in queryIngredientId)
            {
                ShoppingListItem shoppingCartItem = new ShoppingListItem();
                foreach (var item in group)
                {
                    amount += item.amount;
                    shoppingCartItem.IngredientName = item.name;
                    shoppingCartItem.IngredientAisle = item.aisle;
                    shoppingCartItem.IngredientIdList = item._id;
                    shoppingCartItem.IngredientId = item.id;
                    shoppingCartItem.IngredientUnits = item.unit;
                    shoppingCartItem.IngredientImg = item.image;
                }
                shoppingCartItem.StringIngredientAmount = new Fraction(Math.Round(amount, 2)).ToString();
                shoppingCartItem.IngredientAmount = amount;
                amount = 0;
                shoppingCartItem.IsChoose = true;
                shoppingCartItems.Add(shoppingCartItem);
            }
            return Task.FromResult(shoppingCartItems);
        }

        //Delete shopping cart item
        void deleteShoppingCartGroupManagerItem(ShoppingListGroupManager shoppingCartGroupManager)
        {
            shoppingCartGroupAisleBelong.Remove(shoppingCartGroupManager);
        }
        public async Task<bool> DeleteShoppingCartItem(ShoppingListItem shoppingCartItem)
        {
            bool result = false;

            foreach (ItemCart item in originalShoppintCarts.resultsCart)
            {
                if (item.id == shoppingCartItem.IngredientId)
                {
                    Debug.WriteLine(item.name);
                    result = await App.RecipeManager.DeleteShoppingCart(item._id);
                }
            }


            if (result)
            {
                foreach (ShoppingListGroupManager shoppingCartGroupManager in shoppingCartGroupAisleBelong)
                {
                    if (shoppingCartItem.IngredientAisle == shoppingCartGroupManager.Aisle)
                    {
                        shoppingCartGroupManager.ShoppingListItems.Remove(shoppingCartItem);
                        if (shoppingCartGroupManager.ShoppingListItems.Count == 0)
                        {
                            Debug.WriteLine("Empty list");
                            deleteShoppingCartGroupManagerItem(shoppingCartGroupManager);
                            break;
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
