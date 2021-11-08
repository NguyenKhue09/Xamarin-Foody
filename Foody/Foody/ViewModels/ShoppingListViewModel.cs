﻿using Foody.Converters;
using Foody.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Foody.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command Checkmanager { get; }

        public ObservableCollection<ShoppingListGroupManager> shoppingListGroupManagers { get; set; }
        private ShoppingListResult originalShoppintLists { get; set; }

        public ShoppingListViewModel()
        {
            Checkmanager = new Command<string>(manager_SelectionChanged);
            shoppingListGroupManagers = new ObservableCollection<ShoppingListGroupManager>();
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
                        shoppingListGroupManager.shoppingListItems.Remove(shoppingListItem);
                        if (shoppingListGroupManager.shoppingListItems.Count == 0)
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


        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
