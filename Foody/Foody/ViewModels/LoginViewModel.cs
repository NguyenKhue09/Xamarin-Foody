using Foody.Models;
using Foody.Views;
using Foody.Views.PopUp;
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
        private ForgotPasswordPopUp forgotPasswordPopUp;
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


        public LoginViewModel()
        {
            
            _googleManager = DependencyService.Get<IGoogleManager>();
            forgotPasswordPopUp = new ForgotPasswordPopUp();
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

        public void UserLoginGmailPassword(string UserName, string Password)
        {
            _googleManager.LoginGmailPassword(OnLoginComplete, UserName, Password);

        }

        public void RegisterUser(string UserEmail, string Password)
        {
            _googleManager.RegisterUser(OnRegisterComplete, UserEmail, Password);

        }

        public void ResetPassword(string UserEmail)
        {
            _googleManager.ResetPassword(OnResetPassword, UserEmail);

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
                IsLogin = true;

            }
            else
            {
                IsLogin = false;
                
            }
        }

        private void OnRegisterComplete(GoogleUser googleUser, string message)
        {
            Debug.WriteLine("Call onregister compelete");
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

            }
            else
            {
                IsLogin = false;

            }
        }

        private async void OnResetPassword()
        {
            await forgotPasswordPopUp.closeResetPasswordPopup();
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

            }
            else
            {
                IsLogin = false;
               
            }
        }

        async public void NavToHomePage()
        {
            Debug.WriteLine("Navigation");
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
        }
    }
}
