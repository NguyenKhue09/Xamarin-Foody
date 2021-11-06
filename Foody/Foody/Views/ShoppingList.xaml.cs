using System;
using Foody.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            shoppingListViewModel.GetShoppingList();
        }
    }

    
}