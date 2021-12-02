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
        public PageShoppingCart()
        {
            InitializeComponent();
        }

        public PageShoppingCart(ShoppingListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = shoppingListViewModel = viewModel;
            Debug.WriteLine(shoppingListViewModel.shoppingListGroupManagers.Count());

        }

        private void BackToShoppingList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShoppingList());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            shoppingListViewModel.GetShoppingCart();
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Debug.WriteLine("Call Shopping cart");
            foreach (var item in shoppingListViewModel.shoppingCartGroupAisleBelong)
            {
                foreach(ShoppingListItem shoppingListItem in item.shoppingListItems)
                {
                    if (shoppingListItem.IsChoose == false)
                    {
                        Debug.WriteLine(shoppingListItem.IsChoose);
                        bool checkDelete = await shoppingListViewModel.DeleteShoppingCartItem(shoppingListItem);
                    }
                }    
            }    
        }
    }
}