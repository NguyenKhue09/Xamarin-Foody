using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            // check null ObsGoogleUserDetails
            if (App.LoginViewModel.ObsGoogleUserDetails != null && (App.LoginViewModel.ObsGoogleUser.Name == "" || App.LoginViewModel.ObsGoogleUser.Name == null))
            {
                UserName.Text = App.LoginViewModel.ObsGoogleUserDetails.Name;
                UserNameEdit.Text = App.LoginViewModel.ObsGoogleUserDetails.Name;
            } else
            {
                UserName.Text = App.LoginViewModel.ObsGoogleUser.Name;
                UserNameEdit.Text = App.LoginViewModel.ObsGoogleUser.Name;
            }
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Navigation.NavigationStack.Count > 1)
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
        }

        private void UpdateProfile_Tapped(object sender, EventArgs e)
        {
            if(UserNameEdit.Text != null || UserNameEdit.Text != "")
            {
                App.LoginViewModel.UpdateUserDetails(UserNameEdit.Text, App.LoginViewModel.ObsGoogleUser.Picture.ToString());
                if(App.LoginViewModel.IsUpdateDetailSuccess)
                {
                    UserName.Text = UserNameEdit.Text;
                }
                
            }
        }
    }
}