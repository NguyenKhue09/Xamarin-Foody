﻿using Foody.Models;
using Foody.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views.DetailsRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailRecipe : ContentPage
    {
        private readonly DetailRecipeViewModel detailRecipeViewModel;
        public DetailRecipe(Result recipe)
        {
            InitializeComponent();
            BindingContext = detailRecipeViewModel = new DetailRecipeViewModel(recipe);
            //LstInstructions.ItemsSource = recipe.analyzedInstructions[0].steps;
           
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            //var newItemShoppingList = new ItemShoppingList
            //{
            //    aisle = "Baking",
            //    item = "3 package baking powder",
            //};

            //detailRecipeViewModel.AddIngredientsToShoppingList(newItemShoppingList);

            //ShoppingListResult shoppingList = new ShoppingListResult();

            //shoppingList = await App.RecipeManager.GetShoppingList();

            //Debug.WriteLine(shoppingList.cost.ToString());

            //await App.RecipeManager.DeleteShoppingListItem("877471");

            IngredientInform ingredientInform = new IngredientInform();

            ingredientInform = await App.RecipeManager.GetIngredientImg("18369");

            Debug.WriteLine(ingredientInform.image.ToString());

        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           

            try
            {
                //Code to execute on tapped event
                await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TabHost_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {

        }

      
    }
}