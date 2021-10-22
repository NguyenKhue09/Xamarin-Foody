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
            this.CloseWhenBackgroundIsClicked = false;
        }

        public async Task closePopup()
        {
            await Navigation.PopPopupAsync();
        }
    }
}