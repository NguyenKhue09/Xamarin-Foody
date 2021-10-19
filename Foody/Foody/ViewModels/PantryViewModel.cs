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

        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public Command NavToPantry { get; }
        public Command Test { get; }

        public Command ChipSelectedCommand { get; }
        public Command ChipUnselectedCommand { get; }


        INavigation Navigation;

        public PantryViewModel(INavigation MainPageNav)
        {
            Navigation = MainPageNav;
            PopularRecipes = new ObservableRangeCollection<Result>();
            NavToPantry = new Command(async () => await OpenOtherPage(), () => !IsBusy);
            Test = new Command(async () => await showpopup_Clicked(), () => !IsBusy);
            ChipSelectedCommand = new Command<string>(chipSelected);
            ChipUnselectedCommand = new Command<string>(chipUnSelected);
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

        async private Task showpopup_Clicked()
        {
            await Navigation.PushPopupAsync(new SearchPopUp());
        }

        public PantryViewModel()
        {
            SelectedTabIndex = 0;
        }


        public void chipSelected(string value)
        {
            Debug.WriteLine($"S + {value}");

        }

        public void chipUnSelected(string value)
        {
            Debug.WriteLine($"U + {value}");

        }
    }
}
