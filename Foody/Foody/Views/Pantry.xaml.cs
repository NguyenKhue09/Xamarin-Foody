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
            BindingContext = pantryViewModel = new PantryViewModel(Navigation);
            option.IsVisible = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            pantryViewModel.GetPopularRecipes();
            pantryViewModel.GetRandomRecipes();
            pantryViewModel.UserPantryListGroupManagers.Clear();
            pantryViewModel.UserPantryListGroupManagers = await pantryViewModel.GetOriginalPantryBuilderItems();
            if (pantryViewModel.UserPantryListGroupManagers.Count > 0)
            {
                manager.ItemsSource = pantryViewModel.UserPantryListGroupManagers;
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

        private async void deleteSelectedUserPantryItem(object sender, EventArgs e)
        {
            option.IsVisible = !option.IsVisible ? true : false;
            int result = await pantryViewModel.DeleteSelectedUserPantryItem();

            if (result > 0)
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

        private async void deleteAllUserPantryItem(object sender, EventArgs e)
        {
            option.IsVisible = !option.IsVisible ? true : false;
            bool result = await pantryViewModel.DeleteAllUserPantryItem();
            var messageOptions = new MessageOptions
            {
                Foreground = Color.Black,
                Font = Font.SystemFontOfSize(16),
                Message = result ? $"Delete all item successfully!" : $"Delete all item fail!"
            };

            var options = new SnackBarOptions
            {
                MessageOptions = messageOptions,
                Duration = TimeSpan.FromMilliseconds(3000),
                BackgroundColor = result ? Color.FromRgb(75, 181, 67) : Color.FromRgb(250, 17, 61),
                IsRtl = false,
            };
            lb.Height = 0;
            col.Height = 0;
            PTnormal.Height = new GridLength(1, GridUnitType.Star);
            PTlist.Height = 0;
            await this.DisplaySnackBarAsync(options);
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            await Task.Delay(300);
            if(searchBar.Text != null)
            {
                pantryViewModel.SearchUserPantryItem(searchBar.Text);
            }
            
            Debug.WriteLine(pantryViewModel.SearchUserPantryItems.Count);
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
            if (pantryViewModel.UserPantryListGroupManagers.Count > 0)
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
            await this.DisplaySnackBarAsync(options);
        }

        private async void AddIngredientToPUserPantry_Tapped(object sender, EventArgs e)
        {
            if (IngredientsSearch.SelectedItem != null)
            {
                PantryBuilder selectedItem = (PantryBuilder)IngredientsSearch.SelectedItem;

                AddItemToUserPantryImg.Source = "loading.gif";
                if (selectedItem != null)
                {
                    UserPantryItem userPantryItem = new UserPantryItem
                    {
                        userId = App.LoginViewModel.GoogleUser.UID,
                        itemId = selectedItem._id
                    };

                    bool result = await App.RecipeManager.AddItemToUserPantry(userPantryItem);
                    if (result)
                    {
                        AddItemToUserPantryImg.Source = "plus1.png";
                        pantryViewModel.UserPantryListGroupManagers.Clear();
                        pantryViewModel.IsShowSearchIngredientItem = false;
                        pantryViewModel.UserPantryListGroupManagers = await pantryViewModel.GetOriginalPantryBuilderItems();
                        if (pantryViewModel.UserPantryListGroupManagers.Count > 0)
                        {
                            manager.ItemsSource = pantryViewModel.UserPantryListGroupManagers;
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
                        SearchBarIngredient.Text = null;
                    }
                    else
                    {
                        AddItemToUserPantryImg.Source = "plus1.png";
                        await DisplayAlert("Error", "Add item to user pantry fail!", "OK");
                        pantryViewModel.IsShowSearchIngredientItem = false;
                        SearchBarIngredient.Text = null;
                    }
                }
                else
                {
                    AddItemToUserPantryImg.Source = "plus1.png";
                    await DisplayAlert("Error", "Add item to user pantry fail!", "OK");
                    pantryViewModel.IsShowSearchIngredientItem = false;
                    SearchBarIngredient.Text = null;
                }
                if (pantryViewModel.UserPantryListGroupManagers.Count > 0)
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
            IngredientsSearch.SelectedItem = null;
        }
    }
}