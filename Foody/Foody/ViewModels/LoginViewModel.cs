using Foody.Models;
using Foody.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        private readonly IGoogleManager _googleManager;
        public GoogleUser GoogleUser;

        public GoogleUser ObsGoogleUser
        {
            get => GoogleUser;
            set => SetProperty(ref GoogleUser, value);
        }

        public bool isLogin;

        public bool IsLogin
        {
            get => isLogin;
            set => SetProperty(ref isLogin, value);
        }

        public string test;

        public string Test
        {
            get => test;
            set => SetProperty(ref test, value);
        }

        public LoginViewModel()
        {
            Test = "VLVVLVLV";
            _googleManager = DependencyService.Get<IGoogleManager>();
        }

        public async void UserLogout()
        {
            _googleManager.Logout();
            ObsGoogleUser = null;
            IsLogin = false;
            await Shell.Current.GoToAsync("Login", true);
        }
    
        public void UserGoogleLogin()
        {
            _googleManager.Login(OnLoginComplete);

        }

        public void CheckUserLogin()
        {
            _googleManager.CheckUserLogin(IsUserLogin);
        }

        private  void OnLoginComplete(GoogleUser googleUser, string message)
        {
            Debug.WriteLine("Call onlogin compelete");
            if (googleUser != null)
            {
                ObsGoogleUser = new GoogleUser
                {
                    Name = googleUser.Name,
                    Email = googleUser.Email,
                    Picture = googleUser.Picture,
                    UID = googleUser.UID
                };
                Debug.WriteLine(GoogleUser.Name);
                Debug.WriteLine(GoogleUser.Email);
                Debug.WriteLine(GoogleUser.Picture);
                Debug.WriteLine(GoogleUser.UID);
                NavToHomePage();
                Test = googleUser.Name;
                IsLogin = true;

            }
            else
            {
                IsLogin = false;
                Test = "VLVVLVLV";
            }
        }

        private  void IsUserLogin(GoogleUser googleUser)
        {
           
            if (googleUser != null)
            {
                ObsGoogleUser = new GoogleUser
                {
                    Name = googleUser.Name,
                    Email = googleUser.Email,
                    Picture = googleUser.Picture,
                    UID = googleUser.UID
                };
                Debug.WriteLine(GoogleUser.Name);
                Debug.WriteLine(GoogleUser.Email);
                Debug.WriteLine(GoogleUser.Picture);
                Debug.WriteLine(GoogleUser.UID);
                NavToHomePage();
                IsLogin = true;
                Test = googleUser.Name;

            }
            else
            {
                IsLogin = false;
                Test = "VLVVLVLV";
            }
        }

        async public void NavToHomePage()
        {
            Debug.WriteLine("Navigation");
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
        }
    }
}
