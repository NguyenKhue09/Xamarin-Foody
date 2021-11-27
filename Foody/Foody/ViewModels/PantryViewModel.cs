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
        public ObservableRangeCollection<Result> RandomRecipes { get; set; }
        public ObservableRangeCollection<Result> SearchRecipes { get; set; }
        public Command NavToPantry { get; }
        public Command Test { get; }

        public Command ChipCuisineSelectedCommand { get; }
        public Command ChipCuisineUnselectedCommand { get; }

        public Command ChipIntolerancesSelectedCommand { get; }
        public Command ChipIntolerancesUnselectedCommand { get; }

        //
        public ObservableRangeCollection<IngredientInform> SearchIngredients { get; set; }

        public bool isShowSearchIngredientItem = false;

        public string showHeightResultSearch = "0,0,0,0";

        public string ShowHeightResultSearch
        {
            get { return showHeightResultSearch; }
            set { SetProperty(ref showHeightResultSearch, value); }
        }

        public bool IsShowSearchIngredientItem
        {
            get { return isShowSearchIngredientItem; }
            set { SetProperty(ref isShowSearchIngredientItem, value); }
        }

        public Command Checkmanager { get; }

        public ObservableCollection<ShoppingListGroupManager> Groups = new ObservableCollection<ShoppingListGroupManager>{
                new ShoppingListGroupManager("Carbohydrates",
                  new ObservableCollection<ShoppingListItem>{ new ShoppingListItem { IngredientName = "pasta", IngredientAisle = "Carb Snakes" },
                    new ShoppingListItem { IngredientName = "potato", IngredientAisle = "The King of all Carbs" },
                    new ShoppingListItem { IngredientName = "bread", IngredientAisle = "Soft & Gentle"},
                    new ShoppingListItem { IngredientName = "rice", IngredientAisle = "Tiny grains of goodness" },
                 }),
                new ShoppingListGroupManager("Fruits",
                   new ObservableCollection<ShoppingListItem>{ new ShoppingListItem { IngredientName = "apple", IngredientAisle = "Keep the Doctor away"},
                    new ShoppingListItem { IngredientName = "banana", IngredientAisle = "This fruit is appeeling"},
                    new ShoppingListItem { IngredientName = "pear", IngredientAisle = "Pear with me"},
                }),
                new ShoppingListGroupManager("Vegetables",
                   new ObservableCollection<ShoppingListItem>{ new ShoppingListItem { IngredientName = "carrot", IngredientAisle = "Sounds like parrot"},
                    new ShoppingListItem { IngredientName = "green bean", IngredientAisle = "The less popular cousin of the baked bean"},
                    new ShoppingListItem { IngredientName = "broccoli", IngredientAisle = "Tiny food trees"},
                    new ShoppingListItem { IngredientName = "peas", IngredientAisle = "Peas sir, can I have some more?"},
                }),
                new ShoppingListGroupManager("Dairy",
                  new ObservableCollection<ShoppingListItem>{  new ShoppingListItem { IngredientName = "Milk", IngredientAisle = "Molk"},
                    new ShoppingListItem { IngredientName = "Cheese", IngredientAisle = "Cheese + Potato = <3"},
                    new ShoppingListItem { IngredientName = "Ice Cream", IngredientAisle = "Because I couldn't find an icon for yoghurt"},

                })
        };
        public ObservableCollection<ShoppingListGroupManager> manager { get; set; }



        INavigation Navigation;

        public PantryViewModel(INavigation MainPageNav)
        {
            Navigation = MainPageNav;
            PopularRecipes = new ObservableRangeCollection<Result>();
            RandomRecipes = new ObservableRangeCollection<Result>();
            SearchRecipes = new ObservableRangeCollection<Result>();
            NavToPantry = new Command(async () => await OpenOtherPage(), () => !IsBusy);
            Test = new Command(async () => await showpopup_Clicked(), () => !IsBusy);
            ChipCuisineSelectedCommand = new Command<string>(chipCuisineSelected);
            ChipCuisineUnselectedCommand = new Command<string>(chipCuisineUnSelected);
            ChipIntolerancesSelectedCommand = new Command<string>(chipIntolerancesSelected);
            ChipIntolerancesUnselectedCommand = new Command<string>(chipIntolerancesUnSelected);
            intolerancesList = new List<string>();
            cuisineList = new List<string>();
            SearchIngredients = new ObservableRangeCollection<IngredientInform>();
            //
            Checkmanager = new Command<string>(manager_SelectionChanged);
            manager = new ObservableCollection<ShoppingListGroupManager>(Groups);
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

        async public void GetRandomRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetRandomRecipes();
            RandomRecipes.AddRange(results.results);

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
        //
        public void manager_SelectionChanged(string topic)
        {
            changeExpand(topic);
        }
        public void changeExpand(string item)
        {
            foreach (ShoppingListGroupManager group in manager)
            {
                

                if (item == group.Aisle)
                {
                    group.IsExpanded = !group.IsExpanded;
                    group.IconExpand = group.IsExpanded ? "up.png" : "down.png";
                    Debug.WriteLine(group.IconExpand);
                    Debug.WriteLine(item);
                    Debug.WriteLine(group.Aisle);
                }
            }
        }

        //search pantry manager
        async public void SearchIngredient(string searchString)
        {

            SearchIngredientsResult results = await App.RecipeManager.SearchIngredients(searchString);

            if (results != null)
            {
                if (results.results.Count > 0)
                {

                    SearchIngredients.Clear();
                    SearchIngredients.AddRange(results.results);
                    if (results.results.Count == 1)
                    {
                        ShowHeightResultSearch = "0,0,280,65";
                    }
                    else if (results.results.Count == 2)
                    {
                        ShowHeightResultSearch = "0,0,280,125";
                    }
                    else
                    {
                        ShowHeightResultSearch = "0,0,280,185";
                    }
                    IsShowSearchIngredientItem = true;
                }
                else
                {
                    IsShowSearchIngredientItem = false;
                }
            }

        }


    } 
    
}
