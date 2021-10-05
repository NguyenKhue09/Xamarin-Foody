using Foody.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Foody
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(Pantry), typeof(Pantry));
            Routing.RegisterRoute(nameof(ShoppingList), typeof(ShoppingList));
            Routing.RegisterRoute(nameof(MealPlan), typeof(MealPlan));
           
            
        }

    }
}
