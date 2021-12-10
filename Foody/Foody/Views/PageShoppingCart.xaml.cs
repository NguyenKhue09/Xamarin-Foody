using Foody.Data.Local;
using Foody.Models;
using Foody.Models.Local;
using Foody.ViewModels;
using System;
using System.Collections.Generic;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            shoppingListViewModel.GetShoppingCart();

            if(shoppingListViewModel.totalItemShoppingCart > 0)
            {
                shoppingCartNull.Height = 0;
                shoppingCart.Height= new GridLength(3, GridUnitType.Star);
            }
            else
            {
                shoppingCartNull.Height = 200;
                shoppingCart.Height = 0;
            }
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Debug.WriteLine("Call Shopping cart");
            ShoppingListItem shoppingList = new ShoppingListItem();
            
            //foreach (var item in shoppingListViewModel.shoppingCartGroupAisleBelong)
            //{
            //    foreach (ShoppingListItem shoppingCartItem in item.shoppingListItems)
            //    {
            //        Debug.WriteLine(shoppingCartItem.IsChoose);
            //        if (shoppingCartItem.IsChoose == false)
            //        {
            //            Debug.WriteLine("okeoke");
            //            Debug.WriteLine(shoppingCartItem.IsChoose);
            //            Debug.WriteLine("okeoke");
            //            shoppingList = shoppingCartItem;
            //            break;
            //        }
            //    }
            //    Debug.WriteLine("okeoke");
            //    if (shoppingList != null)
            //    {
            //        break;
            //    }
            //}
            //if(shoppingList != null)
            //{
            //    checkDelete = await shoppingListViewModel.DeleteShoppingCartItem(shoppingList);
            //    if (checkDelete)
            //    {
            //        shoppingList = null;
            //    }
            //}   
              
        }

        private void DeleteShoppingCartItem(object sender, EventArgs e)
        {
            
        }
    }
}