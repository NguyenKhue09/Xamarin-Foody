using System;
using Foody.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Foody.Models;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using System.Collections.ObjectModel;
using Foody.Models.Local;
using Foody.Data.Local;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingList : ContentPage
    {
        private readonly ShoppingListViewModel shoppingListViewModel;
        public ShoppingList()
        {
            InitializeComponent();
            BindingContext = shoppingListViewModel = new ShoppingListViewModel();
            option.IsVisible = false;
            showTotalItemShoppingCart.IsVisible = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            shoppingListViewModel.shoppingCartGroupAisleBelong = await shoppingListViewModel.GetShoppingCart();
            if (shoppingListViewModel.shoppingCartGroupAisleBelong.Count > 0)
            {
                int totalItem = 0;
                foreach( var item in shoppingListViewModel.shoppingCartGroupAisleBelong)
                {
                    totalItem += item.shoppingListItems.Count;
                }
                showTotalItemShoppingCart.IsVisible = true;
                totalItemShoppingCart.Text = totalItem.ToString();
            }
            else
            {
                showTotalItemShoppingCart.IsVisible = false;
            }
            shoppingListViewModel.shoppingListGroupManagers = await shoppingListViewModel.GetShoppingList();
            if(shoppingListViewModel.shoppingListGroupManagers.Count >0)
            {
                shoplist.ItemsSource = shoppingListViewModel.shoppingListGroupManagers;
                btnAddToCart.IsVisible = true;
            }
            else
            {
                btnAddToCart.IsVisible=false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // clear để ko bị add dồn phần tử
            shoppingListViewModel.IsSelectedAllShoppingListItem = false;
            shoppingListViewModel.shoppingListGroupManagers.Clear();
        }

        private async void DeleteShoppingListItem(object sender, EventArgs e)
        {
            ShoppingListItem listItem = ((TappedEventArgs)e).Parameter as ShoppingListItem;

            bool result = await shoppingListViewModel.DeleteShoppingListItem(listItem);
            var messageOptions = new MessageOptions
            {
                Foreground = Color.Black,
                Font = Font.SystemFontOfSize(16),
                Message = result ? $"Delete item {listItem.IngredientName} successfully!" : $"Delete item {listItem.IngredientName} fail!"
            };

            var options = new SnackBarOptions
            {
                MessageOptions = messageOptions,
                Duration = TimeSpan.FromMilliseconds(3000),
                BackgroundColor = result ? Color.FromRgb(75, 181, 67) : Color.FromRgb(250, 17, 61),
                IsRtl = false,
            };
            if (shoppingListViewModel.shoppingListGroupManagers.Count > 0)
            {
                btnAddToCart.IsVisible = true;
            }
            else
            {
                btnAddToCart.IsVisible = false;
            }
            await this.DisplaySnackBarAsync(options);
        }

        private void SelectedAllItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //shoppingListViewModel.IsSelectedAllShoppingListItem = !shoppingListViewModel.IsSelectedAllShoppingListItem;
            shoppingListViewModel.SelectAllShoppingListItem();
        }

        private async void Add_To_Cart_Tapped(object sender, EventArgs e)
        {
            shoppingListViewModel.GetSelectedShoppingListItem();
            foreach (ShoppingListItem shoppingListItem in shoppingListViewModel.selectedShoppingtListItems)
            {
                ItemShoppingCart itemShoppingCart = new ItemShoppingCart
                {
                    id = shoppingListItem.IngredientId,
                    aisle = shoppingListItem.IngredientAisle,
                    name = shoppingListItem.IngredientName,
                    amount = shoppingListItem.IngredientAmount,
                    unit = shoppingListItem.IngredientUnits,
                    image = shoppingListItem.IngredientImg,
                    userID = App.LoginViewModel.ObsGoogleUser.UID
                };
                bool checkDelete = await shoppingListViewModel.DeleteShoppingListItem(shoppingListItem);
                if(checkDelete)
                {
                    bool check = await App.RecipeManager.AddIngredientsToShoppingCart(itemShoppingCart);
                }
                if(shoppingListViewModel.selectedShoppingtListItems.Count == 0)
                {
                    break;
                }    
            }
            shoppingListViewModel.selectedShoppingtListItems.Clear();
            if (shoppingListViewModel.shoppingListGroupManagers.Count > 0)
            {
                btnAddToCart.IsVisible = true;
            }
            else
            {
                btnAddToCart.IsVisible = false;
            }
            shoppingListViewModel.shoppingCartGroupAisleBelong = await shoppingListViewModel.GetShoppingCart();
            if (shoppingListViewModel.shoppingCartGroupAisleBelong.Count > 0)
            {
                int totalItem = 0;
                foreach (var item in shoppingListViewModel.shoppingCartGroupAisleBelong)
                {
                    totalItem += item.shoppingListItems.Count;
                }
                totalItemShoppingCart.Text = totalItem.ToString();
                showTotalItemShoppingCart.IsVisible = true;
            }
            else
            {
                showTotalItemShoppingCart.IsVisible = false;
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //shoppingListViewModel.SearchUserPantryItems.Clear();
            SearchBar searchBar = (SearchBar)sender;
            await Task.Delay(300);
            if (searchBar.Text != null)
            {
                shoppingListViewModel.SearchIngredient(searchBar.Text);
            }
           
        }

        private void ShoppingListToShoppingCart(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageShoppingCart(shoppingListViewModel));
        }

        private void ShowOption(object sender, EventArgs e)
        {
            option.IsVisible = !option.IsVisible ? true : false;
        }

        private async void deleteAllSelectedItems(object sender, EventArgs e)
        {
            _ = await shoppingListViewModel.DeleteAllSelectedShoppingListItem();
            option.IsVisible = !option.IsVisible ? true : false;
            btnAddToCart.IsVisible = false;
        }


        private async void AddIngredientToSPL_Tapped(object sender, EventArgs e)
        {
            if (IngredientsSearch.SelectedItem != null)
            {
                IngredientInform selectedItem = (IngredientInform)IngredientsSearch.SelectedItem;
                AddIngredientToSPLImg.Source = "loading.gif";
                IngredientInform ingredientInform = await App.RecipeManager.GetIngredientInform(selectedItem.id);
                if(ingredientInform != null)
                {
                    ItemShoppingList itemShoppingList = new ItemShoppingList
                    {
                        id = ingredientInform.id,
                        aisle = ingredientInform.aisle,
                        name = ingredientInform.name,
                        amount = ingredientInform.amount,
                        unit = ingredientInform.unit,
                        image = ingredientInform.image
                    };
                    bool result  = await App.RecipeManager.AddIngredientsToShoppingList(itemShoppingList);
                    if(result)
                    {
                        AddIngredientToSPLImg.Source = "plus1.png";
                        shoppingListViewModel.shoppingListGroupManagers.Clear();
                        shoppingListViewModel.IsShowSearchIngredientItem = false;
                        shoppingListViewModel.shoppingListGroupManagers = await shoppingListViewModel.GetShoppingList();
                        if (shoppingListViewModel.shoppingListGroupManagers.Count > 0)
                        {
                            shoplist.ItemsSource = shoppingListViewModel.shoppingListGroupManagers;
                            btnAddToCart.IsVisible = true;
                        }
                        else
                        {
                            btnAddToCart.IsVisible = false;
                        }
                        SearchBarIngredient.Text = null;
                    } else
                    {
                        AddIngredientToSPLImg.Source = "plus1.png";
                        await DisplayAlert("Error", "Add CartIngredient to shopping list fail!", "OK");
                        shoppingListViewModel.IsShowSearchIngredientItem = false;
                        SearchBarIngredient.Text = null;
                    }
                } else
                {
                    AddIngredientToSPLImg.Source = "plus1.png";
                    await DisplayAlert("Error", "Add CartIngredient to shopping list fail!", "OK");
                    shoppingListViewModel.IsShowSearchIngredientItem = false;
                    SearchBarIngredient.Text = null;
                }
            }

            IngredientsSearch.SelectedItem = null;
        }
    }

    
}