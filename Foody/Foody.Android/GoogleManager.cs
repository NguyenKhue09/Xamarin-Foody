using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Foody.Droid;
using Foody.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleManager))]
namespace Foody.Droid
{
    
    public class GoogleManager : Java.Lang.Object, IGoogleManager, IOnSuccessListener, IOnFailureListener
    {
        public Action<GoogleUser, string> _onLoginComplete;

        public Action<GoogleUser> _checkUserLogin;
        public Action _resetPassword;
        //public static GoogleApiClient _googleApiClient { get; set; }
        public static GoogleSignInClient _googleApiClient { get; set; }
        public static GoogleManager Instance { get; private set; }
        Context _context;
        FirebaseAuth firebaseAuth;

        public GoogleManager()
        {
            _context = global::Android.App.Application.Context;
            Instance = this;
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                             .RequestIdToken("237009156143-8um28at1u88anpa0fmnqu8ar85jklp11.apps.googleusercontent.com")
                                                             .RequestEmail()
                                                             .RequestProfile()
                                                             .Build();
            //_googleApiClient = new GoogleApiClient.Builder((_context).ApplicationContext)
            //	.AddConnectionCallbacks(this)
            //	.AddOnConnectionFailedListener(this)
            //	.AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
            //	.Build();
            //.AddScope(new Scope(Scopes.Profile))
            _googleApiClient = GoogleSignIn.GetClient(_context, gso);
            firebaseAuth = GetFirebaseAuth();
        }

        private FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(_context);
            FirebaseAuth mAuth;

            if (app == null)
            {
            	var options = new FirebaseOptions.Builder()
            		.SetProjectId("foodyxamarin")
            		.SetApplicationId("foodyxamarin")
            		.SetApiKey("AIzaSyDh_NsI8BjYC_ES970kx5VnP6yC_j8YMc8")
            		.SetDatabaseUrl("https://foodyxamarin.firebaseio.com")
            		.SetStorageBucket("foodyxamarin.appspot.com")
            		.Build();

            	app = FirebaseApp.InitializeApp(_context, options);
                mAuth = FirebaseAuth.Instance;
            }
            else
            {
            	mAuth = FirebaseAuth.Instance;
            }
            return mAuth;
        }

        public void Login(Action<GoogleUser, string> onLoginComplete)
        {
           
            _onLoginComplete = onLoginComplete;
            if (firebaseAuth.CurrentUser == null)
            {
                //Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
                (MainActivity.Instance).StartActivityForResult(_googleApiClient.SignInIntent, 1);
                //_googleApiClient.Connect();
            }
            else
            {
                _onLoginComplete?.Invoke(new GoogleUser
                {
                    Name = firebaseAuth.CurrentUser.DisplayName,
                    Email = firebaseAuth.CurrentUser.Email,
                    Picture = new Uri(firebaseAuth.CurrentUser.PhotoUrl != null ? $"{firebaseAuth.CurrentUser.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"),
                    UID = firebaseAuth.CurrentUser.Uid
                }, string.Empty);
                Toast.MakeText(_context, firebaseAuth.CurrentUser.DisplayName, ToastLength.Long).Show();
            }

        }

        public void LoginGmailPassword(Action<GoogleUser, string> OnLoginGmailPasswordComplete, string UserEmail, string UserPassword)
        {
            _onLoginComplete = OnLoginGmailPasswordComplete;
            LoginGmailPasswordWithFirebase(UserEmail, UserPassword);
        }

        public void RegisterUser(Action<GoogleUser, string> OnRegisterUser, string UserEmail, string UserPassword)
        {
            _onLoginComplete = OnRegisterUser;
            RegisterUserWithFirebase(UserEmail, UserPassword);
        }

        public void ResetPassword(Action OnResetPassword, string UserEmail)
        {
            _resetPassword = OnResetPassword;
            firebaseAuth.SendPasswordResetEmail(UserEmail).AddOnCompleteListener(new HandleOnCompleteListenter(OnResetPasswordSuccess, OnResetPasswordFailure));
        }
        public void RegisterUserWithFirebase(string UserEmail, string Password)
        {
            
            firebaseAuth.CreateUserWithEmailAndPassword(UserEmail, Password).AddOnSuccessListener(this)
                .AddOnFailureListener(this);
        }

        public void LoginWithFirebase(GoogleSignInAccount account)
        {
            var credentials = GoogleAuthProvider.GetCredential(account.IdToken, null);
            firebaseAuth.SignInWithCredential(credentials).AddOnSuccessListener(this)
                .AddOnFailureListener(this);
        }

        public void LoginGmailPasswordWithFirebase(string UserEmail, string UserPassword)
        {   
            firebaseAuth.SignInWithEmailAndPassword(UserEmail, UserPassword).AddOnSuccessListener(this)
                .AddOnFailureListener(this);
        }

        public void CheckUserLogin(Action<GoogleUser> IsLoggedin)
        {
            _checkUserLogin = IsLoggedin;

            if (firebaseAuth.CurrentUser != null)
            {
                _checkUserLogin?.Invoke(
                new GoogleUser
                {
                    Name = firebaseAuth.CurrentUser.DisplayName,
                    Email = firebaseAuth.CurrentUser.Email,
                    Picture = new Uri(firebaseAuth.CurrentUser.PhotoUrl != null ? $"{firebaseAuth.CurrentUser.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"),
                    UID = firebaseAuth.CurrentUser.Uid

                });
            } else
            {
               _checkUserLogin?.Invoke(null);
            }
            
        }

        public void Logout()
        {
            var gsoBuilder = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail();

            GoogleSignIn.GetClient(_context, gsoBuilder.Build())?.SignOut();

            //_googleApiClient.Disconnect();

            firebaseAuth.SignOut();

        }

        public void OnAuthCompleted(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                GoogleSignInAccount accountt = result.SignInAccount;
                _onLoginComplete?.Invoke(new GoogleUser
                {
                    Name = accountt.DisplayName,
                    Email = accountt.Email,
                    Picture = new Uri(accountt.PhotoUrl != null ? $"{accountt.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"),
                    UID = firebaseAuth.CurrentUser.ProviderId
                }, string.Empty);
            }
            else
            {
                _onLoginComplete?.Invoke(null, "An error occured!");
            }
        }

        public void OnConnected(Bundle connectionHint)
        {

        }

        public void OnConnectionSuspended(int cause)
        {
            _onLoginComplete?.Invoke(null, "Canceled!");
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            _onLoginComplete?.Invoke(new GoogleUser
            {
                Name = firebaseAuth.CurrentUser.DisplayName,
                Email = firebaseAuth.CurrentUser.Email,
                Picture = new Uri(firebaseAuth.CurrentUser.PhotoUrl != null ? $"{firebaseAuth.CurrentUser.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"),
                UID = firebaseAuth.CurrentUser.Uid
            }, string.Empty);

            Toast.MakeText(_context, "Login successful", ToastLength.Short).Show();
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(_context, $"Login Failed", ToastLength.Short).Show();
        }

        public void OnResetPasswordSuccess()
        {
            _resetPassword.Invoke();
            Toast.MakeText(_context, "Check your email!", ToastLength.Short).Show();
        }

        public void OnResetPasswordFailure()
        {
            _resetPassword.Invoke();
            Toast.MakeText(_context, $"ResetPassword Failed", ToastLength.Short).Show();
        }

    }

    public class HandleOnCompleteListenter : Java.Lang.Object, IOnCompleteListener
    {
        Context _context;
        private Action OnSuccess;
        private Action OnFailure;
        public HandleOnCompleteListenter(Action success, Action failure)
        {
            _context = global::Android.App.Application.Context;
            OnSuccess = success;
            OnFailure = failure;

        }
        public void OnComplete(Task task)
        {
            if(task.IsSuccessful)
            {
                OnSuccess.Invoke();
            } else
            {
                OnFailure.Invoke();
                Toast.MakeText(_context, task.Exception.Message, ToastLength.Short).Show();
            }
        }
    }
}