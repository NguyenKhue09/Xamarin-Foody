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
    public partial class CreateAccount : ContentPage
    {
        public ICommand Login => new Command(NavToHomePage);
        public CreateAccount()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (App.LoginViewModel.IsLogin)
            {
                await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/menu", true);
            }
        }
        async public void NavToHomePage()
        {

            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);

        }

        private void Signin_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ForgotPasswordPopUp());
        }

        private async void Gmail_Tapped(object sender, EventArgs e)
        {
           
            App.LoginViewModel.UserGoogleLogin();
            if (App.LoginViewModel.IsLogin)
            {
                await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
            }
        }

        private void RegisterUser_Tapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Create Acounnt");
            App.LoginViewModel.RegisterUser(txtUserEmail.Text, Password.Text);
        }
    }
}