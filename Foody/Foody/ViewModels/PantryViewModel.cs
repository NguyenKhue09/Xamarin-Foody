using Foody.Models;
using Foody.Views;
using Foody.Views.PopUp;
using MvvmHelpers;
using Rg.Plugins.Popup.Extensions;
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

       
        private string[] cuisineList { get; set; }
        private string[] intolerancesList { get; set; }

        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public ObservableRangeCollection<Result> SearchRecipes { get; set; }
        public Command NavToPantry { get; }
        public Command Test { get; }

        public Command ChipCuisineSelectedCommand { get; }
        public Command ChipCuisineUnselectedCommand { get; }

        public Command ChipIntolerancesSelectedCommand { get; }
        public Command ChipIntolerancesUnselectedCommand { get; }


        INavigation Navigation;

        public PantryViewModel(INavigation MainPageNav)
        {
            Navigation = MainPageNav;
            PopularRecipes = new ObservableRangeCollection<Result>();
            SearchRecipes = new ObservableRangeCollection<Result>();
            NavToPantry = new Command(async () => await OpenOtherPage(), () => !IsBusy);
            Test = new Command(async () => await showpopup_Clicked(), () => !IsBusy);
            ChipCuisineSelectedCommand = new Command<string>(chipCuisineSelected);
            ChipCuisineUnselectedCommand = new Command<string>(chipCuisineUnSelected);
            ChipIntolerancesSelectedCommand = new Command<string>(chipIntolerancesSelected);
            ChipIntolerancesUnselectedCommand = new Command<string>(chipIntolerancesUnSelected);
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

        async public void GetSearchRecipes(string query, string cuisine, string intolerances)
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.SearchRecipes(query, cuisine, intolerances);
            SearchRecipes.AddRange(results.results);
        }

        async private Task showpopup_Clicked()
        {
            await Navigation.PushPopupAsync(new SearchPopUp());
        }

        public PantryViewModel()
        {
            SelectedTabIndex = 0;
        }


        public void chipCuisineSelected(string value)
        {

            Debug.WriteLine($"S + {value}");
        }

        public void chipCuisineUnSelected(string value)
        {
            Debug.WriteLine($"U + {value}");

        }

        public void chipIntolerancesSelected(string value)
        {

            Debug.WriteLine($"S + {value}");
        }

        public void chipIntolerancesUnSelected(string value)
        {
            Debug.WriteLine($"U + {value}");

        }
    }
}
