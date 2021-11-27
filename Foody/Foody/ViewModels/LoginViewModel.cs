using Foody.Models;
using Foody.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class LoginViewModel
    {
        private readonly IGoogleManager _googleManager;
        public GoogleUser GoogleUser { get; set; }

        public bool isLogin { get; set; }

        public LoginViewModel()
        {
            _googleManager = DependencyService.Get<IGoogleManager>();
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
                GoogleUser = new GoogleUser
                {
                    Name = googleUser.Name,
                    Email = googleUser.Email,
                    Picture = googleUser.Picture,
                };
                Debug.WriteLine(GoogleUser.Name);
                Debug.WriteLine(GoogleUser.Email);
                Debug.WriteLine(GoogleUser.Picture);
                NavToHomePage();
                isLogin = true;

            }
            else
            {
                isLogin = false;
            }
        }

        private  void IsUserLogin(GoogleUser googleUser)
        {
           
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
                NavToHomePage();
                isLogin = true;

            }
            else
            {
                isLogin = false;
            }
        }

        async public void NavToHomePage()
        {
            Debug.WriteLine("Navigation");
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
        }
    }
}
