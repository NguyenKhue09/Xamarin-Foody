using Foody.Models;
using Foody.Views.PopUp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        public Login()
        {
            
            InitializeComponent();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(App.LoginViewModel.IsLogin)
            {
                await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/menu", true);
            }
        }

        async public void NavToHomePage()
        {
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
        }

        async private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync( new ForgotPasswordPopUp());
        }

        private void LoginGmail_Tapped(object sender, EventArgs e)
        {
            App.LoginViewModel.UserGoogleLogin();
        }

        private async void CreateAccount_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void LoginGmailPassword_Tapped(object sender, EventArgs e)
        {
            
            App.LoginViewModel.UserLoginGmailPassword(txtUserEmail.Text, Password.Text);
            if (App.LoginViewModel.IsLogin)
            {
                await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
            }
        }
    }
}