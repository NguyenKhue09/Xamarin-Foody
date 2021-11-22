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
            //shoplist.ItemsSource = shoppingListViewModel.shoppingListGroupManagers;
            Debug.WriteLine(shoppingListViewModel.shoppingListGroupManagers.Count());
            
        }

        private void BackToShoppingList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShoppingList());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //shoppingListViewModel.GetShoppingList();
        }

    }
}