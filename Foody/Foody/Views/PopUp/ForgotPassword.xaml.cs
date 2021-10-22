using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ICommand ResetPassword => new Command(resetpassword);
        public ForgotPassword()
        {
            InitializeComponent();
            BindingContext = this;
        }

        async public void resetpassword()
        {
            await Navigation.PopPopupAsync();
        }
    }
}