using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Foody.Droid.Renderers
{
    public class CustomBottomNavView : IShellBottomNavViewAppearanceTracker
    {

        public void Dispose()
        {

        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {
            

            var parameters = bottomView.LayoutParameters;
            bottomView.ItemTextAppearanceActive = Android.Graphics.Color.Red;
            //bottomView.ItemIconTintList = myList;
            parameters.Height = 200;
            bottomView.LayoutParameters = parameters;
        }

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {

            //int[][] states = new int[][]
            // {
            //    new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
            //    new int[] {-Android.Resource.Attribute.StateEnabled}, // disabled
            //    new int[] {-Android.Resource.Attribute.StateChecked}, // unchecked
            //    new int[] { Android.Resource.Attribute.StateChecked }  // pressed
            // };

            //int[] colors = new int[]
            //{
            //    Xamarin.Forms.Color.Black.ToAndroid(),
            //    Xamarin.Forms.Color.Red.ToAndroid(),
            //    Xamarin.Forms.Color.Green.ToAndroid(),
            //    Xamarin.Forms.Color.Blue.ToAndroid(),
            //    Xamarin.Forms.Color.Blue.ToAndroid(),
            //};

            //ColorStateList myList = new ColorStateList(states, colors);

            //bottomView.SetBackgroundColor(Android.Graphics.Color.ParseColor("#487eb0"));
            // bottomView.SetBackgroundResource(Resource.Drawable.bottombackground);

            //bottomView.SetBackgroundColor(Android.Graphics.Color.ParseColor("#487eb0"));

            var parameters = bottomView.LayoutParameters;
            bottomView.ItemTextAppearanceActive = (global::Android.Graphics.Color.LightGreen);
            parameters.Height = 200;
            bottomView.LayoutParameters = parameters;

            // Change colors on different states
            int[][] states = new int[][]{
                new int[]{-Android.Resource.Attribute.StateChecked},  // unchecked
                new int[]{Android.Resource.Attribute.StateChecked},   // checked
                new int[]{}                                // default
            };

            int[] colors = new int[]{
                Android.Graphics.Color.ParseColor("#747474"),
                Android.Graphics.Color.ParseColor("#007f42"),
                Android.Graphics.Color.ParseColor("#747474"),
            };
            ColorStateList navigationViewColorStateList = new ColorStateList(states, colors);
            bottomView.ItemIconTintList = navigationViewColorStateList;
            bottomView.ItemTextColor = navigationViewColorStateList;
            bottomView.SetPadding(10, 5, 5, 10);
            bottomView.SetShiftMode(true, true);
  
        }

    }
}