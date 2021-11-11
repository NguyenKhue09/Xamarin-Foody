using Foody.ViewModels;
using System;
using System.Collections.Generic;
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
            BindingContext = shoppingListViewModel = new ShoppingListViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShoppingList());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            shoppingListViewModel.GetShoppingList();
        }

    }
}