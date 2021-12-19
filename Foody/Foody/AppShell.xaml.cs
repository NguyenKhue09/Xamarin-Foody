using Foody.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Foody
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Login", typeof(Login));
            Routing.RegisterRoute("Account", typeof(Account));
            Routing.RegisterRoute("FavoriteRecipes", typeof(FavoriteRecipesPage));

        }
    }
}
