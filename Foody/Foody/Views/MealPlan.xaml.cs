﻿using Foody.ViewModels;
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
    public partial class MealPlan : ContentPage
    {
        private readonly MealPlanViewModel mealPlanViewModel;
        public MealPlan()
        {
            InitializeComponent();
            BindingContext = mealPlanViewModel = new MealPlanViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
            if(mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
            {
                BackgroundColor = Color.FromHex("F5F5F5");
                row1.Height = 0;
                row2.Height = new GridLength(1, GridUnitType.Star);
                btnDelete.IsVisible = true;
                containRow1.IsVisible = false;
                if(mealPlanViewModel.userMeal.breakfastRecipe != null)
                {
                    deleteBreakfast.IsVisible = true;
                    resetBreakfast.IsVisible = true;
                    addBreakfast.IsVisible = false;
                    row1Breakfast.Height = 0;
                    row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                    imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                    timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                    titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                    Debug.WriteLine(mealPlanViewModel.userMeal.breakfastRecipe.title);
                }    
                else
                {
                    Debug.WriteLine("Nulll");
                    deleteBreakfast.IsVisible = false;
                    resetBreakfast.IsVisible = false;
                    addBreakfast.IsVisible = true;
                    row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                    row2Breakfast.Height = 0;
                }    
                if(mealPlanViewModel.userMeal.lunchRecipe != null)
                {
                    Debug.WriteLine(mealPlanViewModel.userMeal.lunchRecipe.title);
                }
                if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    Debug.WriteLine(mealPlanViewModel.userMeal.dinnerRecipe.title);
                }
            }
            else
            {
                BackgroundColor = Color.FromHex("FFFFFF");
                row1.Height = new GridLength(1, GridUnitType.Star);
                row2.Height = 0;
                btnDelete.IsVisible = false;
                containRow1.IsVisible = true;
            }
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {

        }

        private void SwipeItem_Invoked_1(object sender, EventArgs e)
        {

        }

        private void SwipeItem_Invoked_2(object sender, EventArgs e)
        {

        }

        private void MealPlantoPageMealTypes_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageMealTypes(mealPlanViewModel));
        }
    }
}