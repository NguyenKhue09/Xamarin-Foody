using Foody.Models;
using Foody.ViewModels;
using Foody.Views.DetailsRecipe;
using Foody.Views.PopUp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pantry : ContentPage
    {
        private readonly PantryViewModel pantryViewModel;

        private string querySearch { get; set; }
        

        public Pantry()
        {
            InitializeComponent();
            CheckFavorite(true);
            BindingContext = pantryViewModel = new PantryViewModel(Navigation);
            option.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pantryViewModel.GetPopularRecipes();
            pantryViewModel.GetRandomRecipes();
            pantryViewModel.UserPantryListGroupManagers.Clear();
            pantryViewModel.GetOriginalPantryBuilderItems();
        }
        void CheckFavorite(bool x)
        {
            if (x)
            {

                lb.Height = new GridLength(0.4, GridUnitType.Star);
                col.Height = new GridLength(0.98, GridUnitType.Star);
                PTnormal.Height = 0;
                PTlist.Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                lb.Height = 0;
                col.Height = 0;
                PTnormal.Height = new GridLength(1, GridUnitType.Star);
                PTlist.Height = 0;
            }

        }
        private void TabPantry_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            if (TabPantry.SelectedIndex == 0)
            {
                headPantry.HeightRequest = 150;
                pantry.IsVisible = false;
                ptManage.IsVisible = true;
                tabPantry.Height = new GridLength(0.4, GridUnitType.Star);
            }
            else
            {
                headPantry.HeightRequest = 70;
                pantry.IsVisible = true;
                ptManage.IsVisible = false;
                tabPantry.Height = new GridLength(0.33, GridUnitType.Star);
            }

        }
        async void PantrySetting(object sender, EventArgs e)
        {
            try
            {
                //Code to execute on tapped event
                await Navigation.PushAsync(new PagePantrySetting());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SearchRecipes(object sender, EventArgs e)
        {
            try
            {
                string intolerancesList;
                string cuisineList;
                cuisineList = String.Join(",", pantryViewModel.cuisineList.ToArray());
                intolerancesList = String.Join(",", pantryViewModel.intolerancesList.ToArray());
                querySearch = searchRecipes.Text;

                pantryViewModel.GetSearchRecipes(querySearch, cuisineList, intolerancesList);

                Debug.WriteLine($"{querySearch}");
                Debug.WriteLine($"{cuisineList}");
                Debug.WriteLine($"{intolerancesList}");
                // call api
                Debug.WriteLine("Call api search");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShowOption(object sender, EventArgs e)
        {
            option.IsVisible = !option.IsVisible ? true : false;
        }

        private void deleteSelectedUserPantryItem(object sender, EventArgs e)
        {
            option.IsVisible = !option.IsVisible ? true : false;
            pantryViewModel.GetSelectedUserPantryItem();
        }

        private void deleteAllUserPantryItem(object sender, EventArgs e)
        {
            option.IsVisible = !option.IsVisible ? true : false;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            await Task.Delay(300);
            pantryViewModel.SearchIngredient(searchBar.Text);
            Debug.WriteLine(pantryViewModel.SearchIngredients.Count);
        }

        private async void collectionView_ingredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView_ingredients.SelectedItem != null)
            {
                Debug.WriteLine("Choose");
                Result recipe = (Result)collectionView_ingredients.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }

            collectionView_ingredients.SelectedItem = null;
        }

        private async void collectionView_Popular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView_Popular.SelectedItem != null)
            {
                Debug.WriteLine("Choose");
                Result recipe = (Result)collectionView_Popular.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }

            collectionView_Popular.SelectedItem = null;
        }

        private async void collectionView_Random_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView_Random.SelectedItem != null)
            {
                Debug.WriteLine("Choose");
                Result recipe = (Result)collectionView_Random.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }

            collectionView_Random.SelectedItem = null;
        }

        private async void DeleteUserPantryItem_Tapped(object sender, EventArgs e)
        {
            ItemId item = ((TappedEventArgs)e).Parameter as ItemId;

            bool result = await pantryViewModel.DeleteUserPantryItem(item);
            var messageOptions = new MessageOptions
            {
                Foreground = Color.Black,
                Font = Font.SystemFontOfSize(16),
                Message = result ? $"Delete {item.name} successfully!" : $"Delete {item.name} fail!"
            };

            var options = new SnackBarOptions
            {
                MessageOptions = messageOptions,
                Duration = TimeSpan.FromMilliseconds(3000),
                BackgroundColor = result ? Color.FromRgb(75, 181, 67) : Color.FromRgb(250, 17, 61),
                IsRtl = false,
            };
            await this.DisplaySnackBarAsync(options);
        }
    }
}