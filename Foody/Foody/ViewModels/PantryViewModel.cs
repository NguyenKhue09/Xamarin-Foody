using Foody.Models;
using Foody.Views;
using Foody.Views.PopUp;
using MvvmHelpers;
using Rg.Plugins.Popup.Extensions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Foody.ViewModels
{
    public class PantryViewModel : BaseViewModel
    {
        public int SelectedTabIndex { get; set; }

        public List<string> cuisineList { get; set; }
        public List<string> intolerancesList { get; set; }

        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public ObservableRangeCollection<Result> SearchRecipes { get; set; }
        public Command NavToPantry { get; }
        public Command Test { get; }

        public Command ChipCuisineSelectedCommand { get; }
        public Command ChipCuisineUnselectedCommand { get; }

        public Command ChipIntolerancesSelectedCommand { get; }
        public Command ChipIntolerancesUnselectedCommand { get; }

        //
        public bool isExpanded;
        public string iconExpand;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { SetProperty(ref isExpanded, value); }
        }

        public string IconExpand
        {
            get { return iconExpand; }
            set { SetProperty(ref iconExpand, value); }
        }

<<<<<<< HEAD
=======
        //
>>>>>>> d6f8144871d5aa4b9d1a67ad39d8e8c2bdb60e53

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
            intolerancesList = new List<string>();
            cuisineList = new List<string>();
            IconExpand = "down.png";
<<<<<<< HEAD
            IsExpanded = false;

=======
            IsExpanded = true;
            changeExpand();
>>>>>>> d6f8144871d5aa4b9d1a67ad39d8e8c2bdb60e53
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
            if(results != null && results.results.Count > 0)
            {
                Debug.WriteLine(results.results.Count.ToString());
                await Navigation.PushAsync(new PageSearchRecipes(results));
            }    
        }

        async public Task showpopup_Clicked()
        {
            await Navigation.PushPopupAsync(new SearchPopUp());
        }


        public PantryViewModel()
        {
            SelectedTabIndex = 0;
        }


        public void chipCuisineSelected(string value)
        {
            if(!cuisineList.Contains(value))
            {
                cuisineList.Add(value);
            }
            Debug.WriteLine($"S + {value}");
        }

        public void chipCuisineUnSelected(string value)
        {
            if(value != "" || value != null)
            {
                cuisineList.Remove(value);
            }
        }

        public void chipIntolerancesSelected(string value)
        {
            if (!intolerancesList.Contains(value))
            {
                intolerancesList.Add(value);
            }
            Debug.WriteLine($"S + {value}");
        }

        public void chipIntolerancesUnSelected(string value)
        {
            if (value != "" || value != null)
            {
                intolerancesList.Remove(value);
            }
            Debug.WriteLine($"U + {value}");

        }
<<<<<<< HEAD

=======
>>>>>>> d6f8144871d5aa4b9d1a67ad39d8e8c2bdb60e53
        public void changeExpand()
        {
            IsExpanded = !IsExpanded;
            IconExpand = IsExpanded ? "up.png" : "down.png";
        }
<<<<<<< HEAD
        
    }
=======
    } 
    
>>>>>>> d6f8144871d5aa4b9d1a67ad39d8e8c2bdb60e53
}
