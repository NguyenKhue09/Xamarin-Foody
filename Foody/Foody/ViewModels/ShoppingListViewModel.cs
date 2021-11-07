﻿using Foody.Converters;
using Foody.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            ShoppingListResult results = new ShoppingListResult();

            results = await App.RecipeManager.GetShoppingList();

            foreach (Aisle aisle in results.aisles)
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
                Console.WriteLine($"Key: {nameGroup.Key}");
                foreach (var item in nameGroup)
                {
                    Console.WriteLine($"\t{item.name}, {item.id}");
                    amount += item.measures.original.amount;
                    shoppingListItem.IngredientName = item.name;
                    shoppingListItem.IngredientAisle = item.aisle;
                    shoppingListItem.IngredientIdList = item.id;
                    shoppingListItem.IngredientUnits = item.measures.original.unit;

                }
                Console.WriteLine($"\t amount: {amount}");
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
