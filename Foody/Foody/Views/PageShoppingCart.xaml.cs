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
                        ShoppingListItem cartItem = shoppingListItem;
                        Debug.WriteLine(shoppingListItem.IsChoose);
                        checkDelete = await shoppingListViewModel.DeleteShoppingCartItem(shoppingListItem);
                        if(checkDelete)
                        {
                            IngredientInform ingredientInform = await App.RecipeManager.GetIngredientInform(cartItem.IngredientId);
                            if (ingredientInform != null)
                            {
                                ItemShoppingList itemShoppingList = new ItemShoppingList
                                {
                                    aisle = ingredientInform.aisle,
                                    parse = true,
                                    item = $"{ingredientInform.amount} {ingredientInform.unit} {ingredientInform.name}"
                                };
                                bool result = await App.RecipeManager.AddIngredientsToShoppingList(itemShoppingList);
                                if (result)
                                {
                                    shoppingListViewModel.shoppingListGroupManagers.Clear();
                                    shoppingListViewModel.GetShoppingList();
                                    Debug.WriteLine("add shoping list from shopping cart thanh cong");
                                }
                                else
                                {
                                    Debug.WriteLine("add shoping list from shopping cart không thanh cong");
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Call API that bai");
                            }
                            break;
                        }
                        Debug.WriteLine(checkDelete);
                    }
                }

                if(checkDelete || item.shoppingListItems.Count == 0)
                { 
                    break;
                }    
                
            }    
        }

        private void DeleteShoppingCartItem(object sender, EventArgs e)
        {
            
        }
    }
}