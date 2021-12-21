using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using FFImageLoading.Svg.Forms;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Widget;
using Android.Views;

namespace Foody.Droid
{
    [Activity(Label = "Foody", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //Window.SetFlags(WindowManagerFlags.TranslucentStatus, WindowManagerFlags.TranslucentStatus);

            Instance = this;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                var task = GoogleSignIn.GetSignedInAccountFromIntent(data);
                try
                {
                    //GoogleSignInAccount result = (GoogleSignInAccount)GoogleSignIn.GetSignedInAccountFromIntent(data).Result;
                    GoogleSignInAccount account = (GoogleSignInAccount)task.Result;
                    if (account != null)
                    {
                        GoogleManager.Instance.LoginWithFirebase(account);
                    }

                }
                catch (Exception e) {
                    Toast.MakeText(this, "Google Sign In Failed", ToastLength.Long).Show();
                }
                //GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                
                
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}