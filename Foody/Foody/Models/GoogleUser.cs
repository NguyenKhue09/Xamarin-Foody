using Foody.Views.PopUp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Foody.Models
{
    public class GoogleUser
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);
        void RegisterUser(Action<GoogleUser, string> OnRegisterUser, string UserEmail, string UserPassword);
        void LoginGmailPassword(Action<GoogleUser, string> OnLoginGmailPasswordComplete, string UserEmail, string UserPassword);
        void CheckUserLogin(Action<GoogleUser> IsLoggedin);
        void ResetPassword(Action OnResetPassword, string UserEmail);
        void Logout();
    }
}
