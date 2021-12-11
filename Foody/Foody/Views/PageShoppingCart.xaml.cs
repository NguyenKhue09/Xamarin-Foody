using Foody.Data.Local;
using Foody.Models;
using Foody.Models.Local;
using Foody.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShoppingCart : ContentPage
    {
        private readonly ShoppingListViewModel shoppingListViewModel;

        private bool checkDelete;
        public PageShoppingCart()
        {
            InitializeComponent();
        }

        public PageShoppingCart(ShoppingListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = shoppingListViewModel = viewModel;
            
            
        }

        private void BackToShoppingList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShoppingList());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            shoppingListViewModel.shoppingCartGroupAisleBelong = await shoppingListViewModel.GetShoppingCart();
            if (shoppingListViewModel.shoppingCartGroupAisleBelong.Count > 0)
            {
                shoppingCartContain.ItemsSource = shoppingListViewModel.shoppingCartGroupAisleBelong;
                shoppingCartNull.Height = 0;
                shoppingCart.Height = new GridLength(3, GridUnitType.Star);
            }
            else
            {
                shoppingCartNull.Height = 200;
                shoppingCart.Height = 0;
            }
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(!e.Value)
            {
                ShoppingListItem shoppingList = new ShoppingListItem();
                foreach (var item in shoppingListViewModel.shoppingCartGroupAisleBelong)
                {
                    foreach (ShoppingListItem shoppingCartItem in item.shoppingListItems)
                    {
                        Debug.WriteLine(shoppingCartItem.IsChoose);
                        if (!shoppingCartItem.isChoose)
                        {
                            Debug.WriteLine("okeoke");
                            Debug.WriteLine(shoppingCartItem.IsChoose);
                            Debug.WriteLine("okeoke");
                            shoppingList = shoppingCartItem;
                            break;
                        }
                    }
                    if (shoppingList != null)
                    {
                        break;
                    }
                }
                if (shoppingList != null)
                {
                    checkDelete = await shoppingListViewModel.DeleteShoppingCartItem(shoppingList);
                    if (checkDelete)
                    {
                        Debug.WriteLine("Ổn r đó");
                    }
                }
            }    

        }

        private async void DeleteShoppingCartItem(object sender, EventArgs e)
        {
            ShoppingListItem listItem = ((TappedEventArgs)e).Parameter as ShoppingListItem;

            bool result = await shoppingListViewModel.DeleteShoppingCartItem(listItem);
            if(result)
            {
                Debug.WriteLine("Delete Item ShoppingCart success");
            }    
        }

        private async void AddShoppingCartToUserPantry_Tapped(object sender, EventArgs e)
        {
            bool result = await App.RecipeManager.AddShoppingCartToUserPantry();

            if(result)
            {
                shoppingListViewModel.shoppingCartGroupAisleBelong.Clear();
                shoppingListViewModel.originalShoppintCarts = null;
            }
        }
    }
}