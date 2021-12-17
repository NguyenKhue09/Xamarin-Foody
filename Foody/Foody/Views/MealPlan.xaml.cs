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
    public partial class MealPlan : ContentPage
    {
        private readonly MealPlanViewModel mealPlanViewModel;
        public MealPlan()
        {
            InitializeComponent();
            BindingContext = mealPlanViewModel = new MealPlanViewModel();
            BackgroundColor = Color.FromHex("F5F5F5");
            row1.Height = 0;
            row2.Height = new GridLength(1, GridUnitType.Star);
        }

        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
            
        //}

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