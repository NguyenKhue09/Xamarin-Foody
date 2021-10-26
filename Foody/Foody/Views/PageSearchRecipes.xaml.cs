using Foody.Models;
using MvvmHelpers;
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
    public partial class PageSearchRecipes : ContentPage
    {
        
        public PageSearchRecipes(Recipe results)
        {
            InitializeComponent();
            BindingContext = results;
        }
        

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pantry());
        }
    }
}