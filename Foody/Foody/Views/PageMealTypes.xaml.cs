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
        public PageMealTypes()
        {
            InitializeComponent();
        }

        private async void BackMealPlan_Tapped(object sender, EventArgs e)
        {
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/mealPlanner", true);
        }
    }
}