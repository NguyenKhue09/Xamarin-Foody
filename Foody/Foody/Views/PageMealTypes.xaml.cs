using Foody.Models;
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
    public partial class PageMealTypes : ContentPage
    {
        private readonly MealPlanViewModel mealPlanViewModel;
        public PageMealTypes(MealPlanViewModel ViewModel)
        {
            InitializeComponent();
            BindingContext = mealPlanViewModel = ViewModel;
        }

        private async void BackMealPlan_Tapped(object sender, EventArgs e)
        {
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        private async void AddRecipetoMealPlan_Tapped(object sender, EventArgs e)
        {
            if(cbBreakfast.IsChecked == true)
            {
                mealPlanViewModel.Breakfast = await mealPlanViewModel.GetMealPlanBreakfast();
            }
            if(cbDinner.IsChecked == true)
            {
                mealPlanViewModel.Dinner = await mealPlanViewModel.GetMealPlanDinner();
            }
            if (cbLunch.IsChecked == true)
            {
                mealPlanViewModel.Lunch = await mealPlanViewModel.GetMealPlanLunch();
            }
            if(mealPlanViewModel.Breakfast.Count > 0 || mealPlanViewModel.Lunch.Count > 0 || mealPlanViewModel.Dinner.Count > 0)
            {
                UserMealPlanItem userMealPlanItem = new UserMealPlanItem();
                userMealPlanItem.userId = App.LoginViewModel.GoogleUser.UID;
                if (mealPlanViewModel.Breakfast.Count > 0)
                {
                    foreach( var item in mealPlanViewModel.Breakfast)
                    {
                        userMealPlanItem.breakfastRecipe = item._id;
                    }
                }
                else
                {
                    userMealPlanItem.breakfastRecipe = null;
                }
                if (mealPlanViewModel.Lunch.Count > 0)
                {
                    foreach (var item in mealPlanViewModel.Lunch)
                    {
                        userMealPlanItem.lunchRecipe = item._id;
                    }
                }
                else
                {
                    userMealPlanItem.lunchRecipe = null;
                }
                if (mealPlanViewModel.Dinner.Count > 0)
                {
                    foreach (var item in mealPlanViewModel.Dinner)
                    {
                        userMealPlanItem.dinnerRecipe = item._id;
                    }
                }
                else
                {
                    userMealPlanItem.dinnerRecipe = null;
                }
                Debug.WriteLine("Check data add");
                Debug.WriteLine(userMealPlanItem.breakfastRecipe);
                Debug.WriteLine(userMealPlanItem.lunchRecipe);
                Debug.WriteLine(userMealPlanItem.dinnerRecipe);
                bool check = await mealPlanViewModel.AddUserMealPlannerItem(userMealPlanItem);
                if (check)
                {
                    Debug.WriteLine("Them thanh cong");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}