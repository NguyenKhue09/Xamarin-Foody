﻿using Foody.Data.Local;
using Foody.Models.Local;
using Foody.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePantrySetting : ContentPage
    {
        private readonly PantrySettingViewModel pantrySettingViewModel;
        public PagePantrySetting()
        {
            InitializeComponent();
            BindingContext = pantrySettingViewModel = new PantrySettingViewModel();
        }

        private void BackToPantry_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pantry());
        }

        //async private void Button_Clicked(object sender, EventArgs e)
        //{
        //    var item = new CartIngredient
        //    {
        //        aisleBelong = "bakery",
        //        ingredientName = "cake",
        //        ingredientImg = ""
        //    };

        //    RecipeDatabase database = await RecipeDatabase.Instance;
        //    await database.SaveIngredientAsync(item);
        //}

        //async private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    RecipeDatabase database = await RecipeDatabase.Instance;
        //    List<CartIngredient> results =  await database.GetIngredientAsync();
        //    Debug.WriteLine(results[0].aisleBelong);
        //}
    }
}