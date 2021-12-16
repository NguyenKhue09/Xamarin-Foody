using Foody.ViewModels;
using System;
using System.Collections.Generic;
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
                if(mealPlanViewModel.Breakfast.Count > 0)
                {

                }
            }
        }
    }
}