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

        void CheckUserLogin(Action<GoogleUser> IsLoggedin);
        void Logout();
    }
}
