using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SearchPopUp()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}