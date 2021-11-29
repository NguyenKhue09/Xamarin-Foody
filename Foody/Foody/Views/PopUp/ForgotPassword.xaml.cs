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
    public partial class ForgotPasswordPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        public ForgotPasswordPopUp()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async Task closeResetPasswordPopup()
        {
            await Navigation.PopPopupAsync();
        }

        private void ResetPassword_Tapped(object sender, EventArgs e)
        {
            App.LoginViewModel.ResetPassword(UserEmail.Text);
        }
    }
}