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
            await this.DisplaySnackBarAsync(options);
        }

        private void SelectedAllItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            shoppingListViewModel.isSelectedAllShoppingListItem = !shoppingListViewModel.isSelectedAllShoppingListItem;
            shoppingListViewModel.SelectAllShoppingListItem();
        }

        private void Add_To_List_Tapped(object sender, EventArgs e)
        {
            shoppingListViewModel.AddShoppingListItemToCard();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            Debug.WriteLine(searchBar.Text);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageShoppingCart(shoppingListViewModel));
        }
    }

    
}