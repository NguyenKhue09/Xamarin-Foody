using Foody.Views.PopUp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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

        async public void NavToHomePage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);

        }

        private void Signin_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ForgotPassword());
        }

        private void Gmail_Tapped(object sender, EventArgs e)
        {

        }
    }
}