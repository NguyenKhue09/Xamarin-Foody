using Foody.Models;
using Foody.ViewModels;
using Foody.Views.PopUp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pantry : ContentPage
    {
        //thu 
        //private ObservableCollection<GroupManage> _allGroups;
        //private ObservableCollection<GroupManage> _expandedGroups;

        private readonly PantryViewModel pantryViewModel;

        private string querySearch { get; set; }

        public Pantry()
        {
            InitializeComponent();
            CheckFavorite(true);
            BindingContext = pantryViewModel = new PantryViewModel(Navigation);
            //_allGroups = GroupManage.All;
            //UpdateListContent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pantryViewModel.GetPopularRecipes();
            //homeViewModel.GetRandomRecipes();
        }
        void CheckFavorite(bool x)
        {
            if (x)
            {

                lb.Height = new GridLength(0.4, GridUnitType.Star);
                col.Height = new GridLength(0.98, GridUnitType.Star);
                PTnormal.Height = 0;
                PTlist.Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                lb.Height = 0;
                col.Height = 0;
                PTnormal.Height = new GridLength(1, GridUnitType.Star);
                PTlist.Height = 0;
            }

        }
        private void TabPantry_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            if (TabPantry.SelectedIndex == 0)
            {
                headPantry.HeightRequest = 150;
                pantry.IsVisible = false;
                ptManage.IsVisible = true;
                tabPantry.Height = new GridLength(0.4, GridUnitType.Star);
            }
            else
            {
                headPantry.HeightRequest = 70;
                pantry.IsVisible = true;
                ptManage.IsVisible = false;
                tabPantry.Height = new GridLength(0.33, GridUnitType.Star);
            }

        }
        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                //Code to execute on tapped event
                await Navigation.PushAsync(new PagePantrySetting());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SearchRecipes(object sender, EventArgs e)
        {
            try
            {
                string intolerancesList;
                string cuisineList;
                cuisineList = String.Join(",", pantryViewModel.cuisineList.ToArray());
                intolerancesList = String.Join(",", pantryViewModel.intolerancesList.ToArray());
                querySearch = searchRecipes.Text;

                pantryViewModel.GetSearchRecipes(querySearch, cuisineList, intolerancesList);

                Debug.WriteLine($"{querySearch}");
                Debug.WriteLine($"{cuisineList}");
                Debug.WriteLine($"{intolerancesList}");
                // call api
                Debug.WriteLine("Call api search");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    int selectedIndex = _expandedGroups.IndexOf(
        //        ((GroupManage)((Button)sender).CommandParameter));
        //    _allGroups[selectedIndex].Expanded = !_allGroups[selectedIndex].Expanded;
        //    UpdateListContent();
        //}
        //private void UpdateListContent()
        //{
        //    _expandedGroups = new ObservableCollection<GroupManage>();
        //    foreach (GroupManage group in _allGroups)
        //    {
        //        //Create new FoodGroups so we do not alter original list
        //        GroupManage newGroup = new GroupManage(group.Title, group.Expanded);
        //        //Add the count of food items for Lits Header Titles to use
        //        newGroup.FoodCount = group.Count;
        //        if (group.Expanded)
        //        {
        //            foreach (Speaker speaker in group)
        //            {
        //                newGroup.Add(speaker);
        //            }
        //        }
        //        _expandedGroups.Add(newGroup);
        //    }
        //    GroupedViewManage.ItemsSource = _expandedGroups;
        //}
    }
}