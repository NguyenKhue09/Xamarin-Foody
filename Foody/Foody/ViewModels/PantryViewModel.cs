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
using System.Collections.ObjectModel;

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
        public ObservableCollection<GroupManager> Groups = new ObservableCollection<GroupManager>{
                new GroupManager("Carbohydrates",
                  new ObservableCollection<Speaker>{ new Speaker { Name = "pasta", Time = "Carb Snakes" },
                    new Speaker { Name = "potato", Time = "The King of all Carbs" },
                    new Speaker { Name = "bread", Time = "Soft & Gentle"},
                    new Speaker { Name = "rice", Time = "Tiny grains of goodness" },
                 }),
                new GroupManager("Fruits",
                   new ObservableCollection<Speaker>{ new Speaker { Name = "apple", Time = "Keep the Doctor away"},
                    new Speaker { Name = "banana", Time = "This fruit is appeeling"},
                    new Speaker { Name = "pear", Time = "Pear with me"},
                }),
                new GroupManager("Vegetables",
                   new ObservableCollection<Speaker>{ new Speaker { Name = "carrot", Time = "Sounds like parrot"},
                    new Speaker { Name = "green bean", Time = "The less popular cousin of the baked bean"},
                    new Speaker { Name = "broccoli", Time = "Tiny food trees"},
                    new Speaker { Name = "peas", Time = "Peas sir, can I have some more?"},
                }),
                new GroupManager("Dairy",
                  new ObservableCollection<Speaker>{  new Speaker { Name = "Milk", Time = "Molk"},
                    new Speaker { Name = "Cheese", Time = "Cheese + Potato = <3"},
                    new Speaker { Name = "Ice Cream", Time = "Because I couldn't find an icon for yoghurt"},

                })
        };
        public ObservableCollection<GroupManager> manager { get; set; }
        //

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
            manager = new ObservableCollection<GroupManager>(Groups);
            changeExpand();
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
            if (results != null && results.results.Count > 0)
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
            if (!cuisineList.Contains(value))
            {
                cuisineList.Add(value);
            }
            Debug.WriteLine($"S + {value}");
        }

        public void chipCuisineUnSelected(string value)
        {
            if (value != "" || value != null)
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
        public void changeExpand()
        {
            foreach (GroupManager group in manager)
            {
                group.IsExpanded = !group.IsExpanded;
                group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
            }
        }

        
    } 
    
}
