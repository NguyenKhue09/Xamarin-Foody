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
        public ICommand login => new Command(NavToHomePage);

        public Login()
        {
            
            InitializeComponent();
            BindingContext = this;
        }

        
        async public void NavToHomePage()
        {
            Debug.WriteLine("Navigation");
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
        }

        async private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync( new ForgotPassword());
        }

        private async void LoginGmail_Tapped(object sender, EventArgs e)
        {
            App.LoginViewModel.UserGoogleLogin();
        }

        async private void CreateAccount_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }
    }
}