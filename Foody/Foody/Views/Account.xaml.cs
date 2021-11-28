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
    public partial class Account : ContentPage
    {
        public Account()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserImg.Source = App.LoginViewModel.ObsGoogleUser.Picture;
            UserEmail.Text = App.LoginViewModel.ObsGoogleUser.Email;
            UserName.Text = App.LoginViewModel.ObsGoogleUser.Name;
            UserNameEdit.Text = App.LoginViewModel.ObsGoogleUser.Name;
        }
    }
}