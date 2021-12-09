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
using Firebase.Firestore;
using Foody.Droid;
using Foody.Models;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleManager))]
namespace Foody.Droid
{

    public class GoogleManager : Java.Lang.Object, IGoogleManager
    {
        public Action<GoogleUser, string> _onLoginComplete;
        public Action<GoogleUser, string> _onGetUserDetailsComplete;

        public Action<GoogleUser> _checkUserLogin;
        public Action _resetPassword;
        public Action<bool> _updateUserDetail;
        //public static GoogleApiClient _googleApiClient { get; set; }
        public static GoogleSignInClient _googleApiClient { get; set; }
        public static GoogleManager Instance { get; private set; }
        Context _context;
        FirebaseAuth firebaseAuth;

        private bool isRegister {get; set;}

        public GoogleManager()
        {
            _context = Android.App.Application.Context;
            Instance = this;
            isRegister = false;
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
            isRegister = true;
            _onLoginComplete = OnRegisterUser;
            RegisterUserWithFirebase(UserEmail, UserPassword);
        }

        private void SaveUserDetailToFireStore(GoogleUser googleUser)
        {
            var userData = new Dictionary<string, Java.Lang.Object>
            {
                { "UID", firebaseAuth.CurrentUser.Uid },
                { "Name", googleUser.Name == null ? googleUser.Email : googleUser.Name},
                { "Email", googleUser.Email },
                { "Picture", googleUser.Picture.ToString() }
            };

            FirebaseFirestore.Instance
                .Collection("Users")
                .Document(firebaseAuth.CurrentUser.Uid)
                .Set(new HashMap(userData), SetOptions.Merge());
        }

        private void UpdateUserDetailToFireStore(string UserName, string UserImg)
        {
            FirebaseFirestore.Instance
                .Collection("Users")
                .Document(firebaseAuth.CurrentUser.Uid)
                .Update("Name", UserName, "Picture", UserImg)
                .AddOnSuccessListener(new HandleOnSuccess(OnUpdateUserDetailSuccess))
                .AddOnFailureListener(new HandleOnFailure(OnUpdateUserDetailFailure));
        }

        public void UpdateUserDetail(Action<bool> OnUpdateUserDetail, string UserName, string UserImg)
        {
            _updateUserDetail = OnUpdateUserDetail;
            UpdateUserDetailToFireStore(UserName, UserImg);
        }

        public void GetUserDetails(Action<GoogleUser, string> OnGetUserDetailsComplete)
        {
            _onGetUserDetailsComplete = OnGetUserDetailsComplete;
            GetUserDetailFromFireStore();
        }

        private void GetUserDetailFromFireStore()
        {
            if (firebaseAuth.CurrentUser != null)
            {
                FirebaseFirestore.Instance
                .Collection("Users")
                .Document(firebaseAuth.CurrentUser.Uid)
                .Get()
                .AddOnSuccessListener(new HandleOnSuccess(OnGetUserDetailSuccess))
                .AddOnFailureListener(new HandleOnFailure(OnGetUserDetailFailure));
            }
        }

        public void ResetPassword(Action OnResetPassword, string UserEmail)
        {
            _resetPassword = OnResetPassword;
            firebaseAuth.SendPasswordResetEmail(UserEmail).AddOnCompleteListener(new HandleOnCompleteListenter(OnResetPasswordSuccess, OnResetPasswordFailure));
        }
        public void RegisterUserWithFirebase(string UserEmail, string Password)
        {
            if(UserEmail != null && Password != null)
            {
                firebaseAuth.CreateUserWithEmailAndPassword(UserEmail, Password).AddOnSuccessListener(new HandleOnSuccess(OnSuccess))
                .AddOnFailureListener(new HandleOnFailure(OnFailure));
            } else
            {
                Toast.MakeText(_context, "Please type your UserEmail/Password!", ToastLength.Long).Show();
            }
            
        }

        public void LoginWithFirebase(GoogleSignInAccount account)
        {
            var credentials = GoogleAuthProvider.GetCredential(account.IdToken, null);
            firebaseAuth.SignInWithCredential(credentials).AddOnSuccessListener(new HandleOnSuccess(OnSuccess))
                .AddOnFailureListener(new HandleOnFailure(OnFailure));
        }

        public void LoginGmailPasswordWithFirebase(string UserEmail, string UserPassword)
        {
            if (UserEmail != null && UserPassword != null)
            {
                firebaseAuth.SignInWithEmailAndPassword(UserEmail, UserPassword).AddOnSuccessListener(new HandleOnSuccess(OnSuccess))
               .AddOnFailureListener(new HandleOnFailure(OnFailure));
            }
            else
            {
                Toast.MakeText(_context, "Please type your UserEmail/Password!", ToastLength.Long).Show();
            }
           
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
                    UID = firebaseAuth.CurrentUser.Uid
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
            GoogleUser userData = new GoogleUser
            {
                Name = firebaseAuth.CurrentUser.DisplayName,
                Email = firebaseAuth.CurrentUser.Email,
                Picture = new Uri(firebaseAuth.CurrentUser.PhotoUrl != null ? $"{firebaseAuth.CurrentUser.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"),
                UID = firebaseAuth.CurrentUser.Uid
            };

            _onLoginComplete?.Invoke(userData, string.Empty);
            if(isRegister)
            {
                SaveUserDetailToFireStore(userData);
                Toast.MakeText(_context, "Register successful", ToastLength.Short).Show();
                isRegister = false;
            } else
            {
                Toast.MakeText(_context, "Login successful", ToastLength.Short).Show();
            }
            
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            if (isRegister)
            {
                Toast.MakeText(_context, $"Register Failed", ToastLength.Short).Show();
                isRegister = false;
            } else
            {
                Toast.MakeText(_context, $"Login Failed", ToastLength.Short).Show();
            }
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

        public void OnGetUserDetailSuccess(Java.Lang.Object result)
        {

            if (result is DocumentSnapshot docRef)
            {
                //Toast.MakeText(_context, $"{docRef.Get("Name")} + {docRef.Exists()}", ToastLength.Long).Show();
                if (docRef.Exists())
                {
                    _onGetUserDetailsComplete.Invoke(
                        new GoogleUser
                        {
                            Name = docRef.Get("Name").ToString(),
                            UID = docRef.Get("UID").ToString(),
                            Email = docRef.Get("Email").ToString(),
                            Picture = new Uri(docRef.Get("Picture").ToString()),
                        }
                        , string.Empty);
                }
            }

            
        }

        public void OnGetUserDetailFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(_context, $"Get user detail Failed", ToastLength.Short).Show();
            _onGetUserDetailsComplete.Invoke(null, string.Empty);
        }

        public void OnUpdateUserDetailSuccess(Java.Lang.Object result)
        {
            _updateUserDetail.Invoke(true);
            Toast.MakeText(_context, $"Update user detail success", ToastLength.Short).Show();
        }

        public void OnUpdateUserDetailFailure(Java.Lang.Exception e)
        {
            _updateUserDetail.Invoke(false);
            Toast.MakeText(_context, $"Update user detail failure", ToastLength.Short).Show();
        }


    }

    public class HandleOnCompleteListenter : Java.Lang.Object, IOnCompleteListener
    {
        Context _context;
        private Action OnSuccess;
        private Action OnFailure;
        public HandleOnCompleteListenter(Action success, Action failure)
        {
            _context = Android.App.Application.Context;
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
                //Toast.MakeText(_context, task.Exception.Message, ToastLength.Short).Show();
            }
        }
    }

    public class HandleOnSuccess : Java.Lang.Object, IOnSuccessListener
    {
        Context _context;

        private Action<Java.Lang.Object> OnSuccessCall;
        public HandleOnSuccess(Action<Java.Lang.Object> success)
        {
            _context = Android.App.Application.Context;
            OnSuccessCall = success;
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            OnSuccessCall.Invoke(result);
        }
    }

    public class HandleOnFailure : Java.Lang.Object, IOnFailureListener
    {
        Context _context;

        private Action<Java.Lang.Exception> OnFailureCall;
        public HandleOnFailure(Action<Java.Lang.Exception> failure)
        {
            _context = Android.App.Application.Context;
            OnFailureCall = failure;
        }
        public void OnFailure(Java.Lang.Exception e)
        {
            OnFailureCall.Invoke(e);
        }
    }
}