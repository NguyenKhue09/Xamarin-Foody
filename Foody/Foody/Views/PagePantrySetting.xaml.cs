using Foody.Data.Local;
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
    public partial class PagePantrySetting : ContentPage
    {
        private readonly PantrySettingViewModel pantrySettingViewModel;
        public PagePantrySetting()
        {
            InitializeComponent();
            BindingContext = pantrySettingViewModel = new PantrySettingViewModel();
            pantrySettingViewModel.GetOriginalPantryBuilderItems();
        }

        private void BackToPantry_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pantry());
        }

        private async void Search_PantryBuilder_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(300);
            pantrySettingViewModel.SearchOriginalPantryBuilderItems(SearchPantryBuilder.Text);
            
        }

        private void AddSelectedToPantry_Tapped(object sender, EventArgs e)
        {
            pantrySettingViewModel.GetSelectedPantryBuidlerItem();
        }
    }
}