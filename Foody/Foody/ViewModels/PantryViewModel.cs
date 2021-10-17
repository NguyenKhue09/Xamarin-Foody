using Foody.Models;
using Foody.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class PantryViewModel : BaseViewModel
    {
        public int SelectedTabIndex { get; set; }

        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public Command NavToPantry { get; }
        INavigation Navigation;

        public PantryViewModel(INavigation MainPageNav)
        {
            Navigation = MainPageNav;
            PopularRecipes = new ObservableRangeCollection<Result>();
            NavToPantry = new Command(async () => await OpenOtherPage(), () => !IsBusy);
        }

        public async Task OpenOtherPage()
        {
            await Navigation.PushAsync(new PagePantrySetting());
        }

        async public void GetPopularRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetPopularRecipes();
            PopularRecipes.AddRange(results.results);
        }

        public PantryViewModel()
        {
            SelectedTabIndex = 0;
        }

    }
}
