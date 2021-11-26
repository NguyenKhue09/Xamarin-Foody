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

        private readonly IGoogleManager _googleManager;
        private GoogleUser GoogleUser { get; set; }
        public Login()
        {
            _googleManager = DependencyService.Get<IGoogleManager>();
            InitializeComponent();
            BindingContext = this;
            CheckUserLoggedIn();
        }

        private void CheckUserLoggedIn()
        {
            _googleManager.Login(OnLoginComplete);
        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            Debug.WriteLine("Call onlogin compelete");
            if (googleUser != null)
            {
                GoogleUser = new GoogleUser
                {
                    Name = googleUser.Name,
                    Email = googleUser.Email,
                    Picture = googleUser.Picture,
                };
                Debug.WriteLine(GoogleUser.Name);
                Debug.WriteLine(GoogleUser.Email);
                Debug.WriteLine(GoogleUser.Picture);
            }
            else
            {
                DisplayAlert("Message", message, "Ok");
            }
        }

        async public void NavToHomePage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);

        }

        async private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync( new ForgotPassword());
        }

        private void LoginGmail_Tapped(object sender, EventArgs e)
        {
            //_googleManager.Login(OnLoginComplete);
        }

        async private void CreateAccount_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }
    }
}