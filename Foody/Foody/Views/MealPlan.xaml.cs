using Foody.Models;
using Foody.ViewModels;
using Foody.Views.DetailsRecipe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealPlan : ContentPage
    {
        private readonly MealPlanViewModel mealPlanViewModel;
        public MealPlan()
        {
            InitializeComponent();
            BindingContext = mealPlanViewModel = new MealPlanViewModel();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
            
            if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
            {
                BackgroundColor = Color.FromHex("F5F5F5");
                row1.Height = 0;
                row2.Height = new GridLength(1, GridUnitType.Star);
                btnDelete.IsVisible = true;
                containRow1.IsVisible = false;
                if(mealPlanViewModel.userMeal.breakfastRecipe != null)
                {
                    deleteBreakfast.IsVisible = true;
                    resetBreakfast.IsVisible = true;
                    addBreakfast.IsVisible = false;
                    row1Breakfast.Height = 0;
                    row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                    imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                    timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                    titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                }    
                else
                {
                    deleteBreakfast.IsVisible = false;
                    resetBreakfast.IsVisible = false;
                    addBreakfast.IsVisible = true;
                    row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                    row2Breakfast.Height = 0;
                }    
                if(mealPlanViewModel.userMeal.lunchRecipe != null)
                {
                    deleteLunch.IsVisible = true;
                    resetLunch.IsVisible = true;
                    addLunch.IsVisible = false;
                    row1Lunch.Height = 0;
                    row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                    imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                    timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                    titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                }
                else
                {
                    deleteLunch.IsVisible = false;
                    resetLunch.IsVisible = false;
                    addLunch.IsVisible = true;
                    row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                    row2Lunch.Height = 0;
                }
                if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    deletebinner.IsVisible = true;
                    resetDinner.IsVisible = true;
                    addDinner.IsVisible = false;
                    row1Dinner.Height = 0;
                    row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                    imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                    timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                    titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                }
                else
                {
                    deletebinner.IsVisible = false;
                    resetDinner.IsVisible = false;
                    addDinner.IsVisible = true;
                    row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                    row2Dinner.Height = 0;
                }
            }
            else
            {
                BackgroundColor = Color.FromHex("FFFFFF");
                row1.Height = new GridLength(1, GridUnitType.Star);
                row2.Height = 0;
                btnDelete.IsVisible = false;
                containRow1.IsVisible = true;
            }
        }

        private async void deleteBreakfast_Invoked(object sender, EventArgs e)
        {
            bool check = await mealPlanViewModel.DeleteUserMealPlanItem("breakfast");
            if (check)
            {
                mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    BackgroundColor = Color.FromHex("F5F5F5");
                    row1.Height = 0;
                    row2.Height = new GridLength(1, GridUnitType.Star);
                    btnDelete.IsVisible = true;
                    containRow1.IsVisible = false;
                    if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                    {
                        deleteBreakfast.IsVisible = true;
                        resetBreakfast.IsVisible = true;
                        addBreakfast.IsVisible = false;
                        row1Breakfast.Height = 0;
                        row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                        timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                        titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                    }
                    else
                    {
                        deleteBreakfast.IsVisible = false;
                        resetBreakfast.IsVisible = false;
                        addBreakfast.IsVisible = true;
                        row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        row2Breakfast.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.lunchRecipe != null)
                    {
                        deleteLunch.IsVisible = true;
                        resetLunch.IsVisible = true;
                        addLunch.IsVisible = false;
                        row1Lunch.Height = 0;
                        row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                        imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                        timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                        titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                    }
                    else
                    {
                        deleteLunch.IsVisible = false;
                        resetLunch.IsVisible = false;
                        addLunch.IsVisible = true;
                        row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                        row2Lunch.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        deletebinner.IsVisible = true;
                        resetDinner.IsVisible = true;
                        addDinner.IsVisible = false;
                        row1Dinner.Height = 0;
                        row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                        imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                        timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                        titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                    }
                    else
                    {
                        deletebinner.IsVisible = false;
                        resetDinner.IsVisible = false;
                        addDinner.IsVisible = true;
                        row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                        row2Dinner.Height = 0;
                    }
                }
                else
                {
                    BackgroundColor = Color.FromHex("FFFFFF");
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = 0;
                    btnDelete.IsVisible = false;
                    containRow1.IsVisible = true;
                }
                mealPlanViewModel.userMeal.breakfastRecipe = null;
            }
        }

        private async void resetBreakfast_Invoked(object sender, EventArgs e)
        {
            if(!mealPlanViewModel.IsLoadingPopupRunning)
            {
                mealPlanViewModel.IsLoadingPopupRunning = true;
                mealPlanViewModel.showLoadingPopup();

                UserMealPlanItem userMealPlanItem = new UserMealPlanItem();
                userMealPlanItem.userId = App.LoginViewModel.GoogleUser.UID;
                mealPlanViewModel.Breakfast.Clear();
                mealPlanViewModel.Breakfast = await mealPlanViewModel.GetMealPlanBreakfast();
                if (mealPlanViewModel.Breakfast.Count > 0)
                {
                    foreach (var item in mealPlanViewModel.Breakfast)
                    {
                        userMealPlanItem.breakfastRecipe = item._id;
                    }
                }
                bool check = await mealPlanViewModel.AddUserMealPlannerItem(userMealPlanItem);
                if (check)
                {
                    mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                    mealPlanViewModel.closeLoadindPopup();

                    if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        BackgroundColor = Color.FromHex("F5F5F5");
                        row1.Height = 0;
                        row2.Height = new GridLength(1, GridUnitType.Star);
                        btnDelete.IsVisible = true;
                        containRow1.IsVisible = false;
                        if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                        {
                            deleteBreakfast.IsVisible = true;
                            resetBreakfast.IsVisible = true;
                            addBreakfast.IsVisible = false;
                            row1Breakfast.Height = 0;
                            row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                            imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                            timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                            titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                        }
                        else
                        {
                            deleteBreakfast.IsVisible = false;
                            resetBreakfast.IsVisible = false;
                            addBreakfast.IsVisible = true;
                            row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                            row2Breakfast.Height = 0;
                        }
                        if (mealPlanViewModel.userMeal.lunchRecipe != null)
                        {
                            deleteLunch.IsVisible = true;
                            resetLunch.IsVisible = true;
                            addLunch.IsVisible = false;
                            row1Lunch.Height = 0;
                            row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                            imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                            timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                            titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                        }
                        else
                        {
                            deleteLunch.IsVisible = false;
                            resetLunch.IsVisible = false;
                            addLunch.IsVisible = true;
                            row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                            row2Lunch.Height = 0;
                        }
                        if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                        {
                            deletebinner.IsVisible = true;
                            resetDinner.IsVisible = true;
                            addDinner.IsVisible = false;
                            row1Dinner.Height = 0;
                            row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                            imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                            timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                            titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                        }
                        else
                        {
                            deletebinner.IsVisible = false;
                            resetDinner.IsVisible = false;
                            addDinner.IsVisible = true;
                            row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                            row2Dinner.Height = 0;
                        }
                    }
                    else
                    {
                        BackgroundColor = Color.FromHex("FFFFFF");
                        row1.Height = new GridLength(1, GridUnitType.Star);
                        row2.Height = 0;
                        btnDelete.IsVisible = false;
                        containRow1.IsVisible = true;
                    }
                } else
                {
                    mealPlanViewModel.closeLoadindPopup();
                }
            }
        }

        private void MealPlantoPageMealTypes_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageMealTypes(mealPlanViewModel));
        }

        private async void DeleteAll_Tapped(object sender, EventArgs e)
        {
            bool check = await mealPlanViewModel.DeleteUserMealPlanItem("all");
            if (check)
            {
                mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    BackgroundColor = Color.FromHex("F5F5F5");
                    row1.Height = 0;
                    row2.Height = new GridLength(1, GridUnitType.Star);
                    btnDelete.IsVisible = true;
                    containRow1.IsVisible = false;
                    if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                    {
                        deleteBreakfast.IsVisible = true;
                        resetBreakfast.IsVisible = true;
                        addBreakfast.IsVisible = false;
                        row1Breakfast.Height = 0;
                        row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                        timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                        titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                    }
                    else
                    {
                        deleteBreakfast.IsVisible = false;
                        resetBreakfast.IsVisible = false;
                        addBreakfast.IsVisible = true;
                        row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        row2Breakfast.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.lunchRecipe != null)
                    {
                        deleteLunch.IsVisible = true;
                        resetLunch.IsVisible = true;
                        addLunch.IsVisible = false;
                        row1Lunch.Height = 0;
                        row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                        imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                        timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                        titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                    }
                    else
                    {
                        deleteLunch.IsVisible = false;
                        resetLunch.IsVisible = false;
                        addLunch.IsVisible = true;
                        row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                        row2Lunch.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        deletebinner.IsVisible = true;
                        resetDinner.IsVisible = true;
                        addDinner.IsVisible = false;
                        row1Dinner.Height = 0;
                        row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                        imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                        timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                        titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                    }
                    else
                    {
                        deletebinner.IsVisible = false;
                        resetDinner.IsVisible = false;
                        addDinner.IsVisible = true;
                        row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                        row2Dinner.Height = 0;
                    }
                }
                else
                {
                    BackgroundColor = Color.FromHex("FFFFFF");
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = 0;
                    btnDelete.IsVisible = false;
                    containRow1.IsVisible = true;
                }
            }
        }

        private async void deleteLunch_Invoked(object sender, EventArgs e)
        {
            bool check = await mealPlanViewModel.DeleteUserMealPlanItem("lunch");
            if (check)
            {
                mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    BackgroundColor = Color.FromHex("F5F5F5");
                    row1.Height = 0;
                    row2.Height = new GridLength(1, GridUnitType.Star);
                    btnDelete.IsVisible = true;
                    containRow1.IsVisible = false;
                    if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                    {
                        deleteBreakfast.IsVisible = true;
                        resetBreakfast.IsVisible = true;
                        addBreakfast.IsVisible = false;
                        row1Breakfast.Height = 0;
                        row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                        timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                        titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                    }
                    else
                    {
                        deleteBreakfast.IsVisible = false;
                        resetBreakfast.IsVisible = false;
                        addBreakfast.IsVisible = true;
                        row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        row2Breakfast.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.lunchRecipe != null)
                    {
                        deleteLunch.IsVisible = true;
                        resetLunch.IsVisible = true;
                        addLunch.IsVisible = false;
                        row1Lunch.Height = 0;
                        row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                        imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                        timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                        titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                    }
                    else
                    {
                        deleteLunch.IsVisible = false;
                        resetLunch.IsVisible = false;
                        addLunch.IsVisible = true;
                        row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                        row2Lunch.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        deletebinner.IsVisible = true;
                        resetDinner.IsVisible = true;
                        addDinner.IsVisible = false;
                        row1Dinner.Height = 0;
                        row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                        imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                        timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                        titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                    }
                    else
                    {
                        deletebinner.IsVisible = false;
                        resetDinner.IsVisible = false;
                        addDinner.IsVisible = true;
                        row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                        row2Dinner.Height = 0;
                    }
                }
                else
                {
                    BackgroundColor = Color.FromHex("FFFFFF");
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = 0;
                    btnDelete.IsVisible = false;
                    containRow1.IsVisible = true;
                }
                mealPlanViewModel.userMeal.lunchRecipe = null;
            }
        }

        private async void resetLunch_Invoked(object sender, EventArgs e)
        {
            if (!mealPlanViewModel.IsLoadingPopupRunning)
            {
                mealPlanViewModel.IsLoadingPopupRunning = true;
                mealPlanViewModel.showLoadingPopup();

                UserMealPlanItem userMealPlanItem = new UserMealPlanItem();
                userMealPlanItem.userId = App.LoginViewModel.GoogleUser.UID;
                mealPlanViewModel.Lunch.Clear();
                mealPlanViewModel.Lunch = await mealPlanViewModel.GetMealPlanLunch();
                if (mealPlanViewModel.Lunch.Count > 0)
                {
                    foreach (var item in mealPlanViewModel.Lunch)
                    {
                        userMealPlanItem.lunchRecipe = item._id;
                    }
                }
                bool check = await mealPlanViewModel.AddUserMealPlannerItem(userMealPlanItem);
                if (check)
                {
                    mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                    mealPlanViewModel.closeLoadindPopup();

                    if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        BackgroundColor = Color.FromHex("F5F5F5");
                        row1.Height = 0;
                        row2.Height = new GridLength(1, GridUnitType.Star);
                        btnDelete.IsVisible = true;
                        containRow1.IsVisible = false;
                        if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                        {
                            deleteBreakfast.IsVisible = true;
                            resetBreakfast.IsVisible = true;
                            addBreakfast.IsVisible = false;
                            row1Breakfast.Height = 0;
                            row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                            imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                            timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                            titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                        }
                        else
                        {
                            deleteBreakfast.IsVisible = false;
                            resetBreakfast.IsVisible = false;
                            addBreakfast.IsVisible = true;
                            row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                            row2Breakfast.Height = 0;
                        }
                        if (mealPlanViewModel.userMeal.lunchRecipe != null)
                        {
                            deleteLunch.IsVisible = true;
                            resetLunch.IsVisible = true;
                            addLunch.IsVisible = false;
                            row1Lunch.Height = 0;
                            row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                            imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                            timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                            titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                        }
                        else
                        {
                            deleteLunch.IsVisible = false;
                            resetLunch.IsVisible = false;
                            addLunch.IsVisible = true;
                            row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                            row2Lunch.Height = 0;
                        }
                        if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                        {
                            deletebinner.IsVisible = true;
                            resetDinner.IsVisible = true;
                            addDinner.IsVisible = false;
                            row1Dinner.Height = 0;
                            row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                            imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                            timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                            titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                        }
                        else
                        {
                            deletebinner.IsVisible = false;
                            resetDinner.IsVisible = false;
                            addDinner.IsVisible = true;
                            row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                            row2Dinner.Height = 0;
                        }
                    }
                    else
                    {
                        BackgroundColor = Color.FromHex("FFFFFF");
                        row1.Height = new GridLength(1, GridUnitType.Star);
                        row2.Height = 0;
                        btnDelete.IsVisible = false;
                        containRow1.IsVisible = true;
                    }
                } else
                {
                    mealPlanViewModel.closeLoadindPopup();
                }
            }
            
        }

        private async void deletebinner_Invoked(object sender, EventArgs e)
        {
            bool check = await mealPlanViewModel.DeleteUserMealPlanItem("dinner");
            if (check)
            {
                mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    BackgroundColor = Color.FromHex("F5F5F5");
                    row1.Height = 0;
                    row2.Height = new GridLength(1, GridUnitType.Star);
                    btnDelete.IsVisible = true;
                    containRow1.IsVisible = false;
                    if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                    {
                        deleteBreakfast.IsVisible = true;
                        resetBreakfast.IsVisible = true;
                        addBreakfast.IsVisible = false;
                        row1Breakfast.Height = 0;
                        row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                        timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                        titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                    }
                    else
                    {
                        deleteBreakfast.IsVisible = false;
                        resetBreakfast.IsVisible = false;
                        addBreakfast.IsVisible = true;
                        row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                        row2Breakfast.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.lunchRecipe != null)
                    {
                        deleteLunch.IsVisible = true;
                        resetLunch.IsVisible = true;
                        addLunch.IsVisible = false;
                        row1Lunch.Height = 0;
                        row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                        imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                        timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                        titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                    }
                    else
                    {
                        deleteLunch.IsVisible = false;
                        resetLunch.IsVisible = false;
                        addLunch.IsVisible = true;
                        row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                        row2Lunch.Height = 0;
                    }
                    if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        deletebinner.IsVisible = true;
                        resetDinner.IsVisible = true;
                        addDinner.IsVisible = false;
                        row1Dinner.Height = 0;
                        row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                        imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                        timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                        titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                    }
                    else
                    {
                        deletebinner.IsVisible = false;
                        resetDinner.IsVisible = false;
                        addDinner.IsVisible = true;
                        row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                        row2Dinner.Height = 0;
                    }
                }
                else
                {
                    BackgroundColor = Color.FromHex("FFFFFF");
                    row1.Height = new GridLength(1, GridUnitType.Star);
                    row2.Height = 0;
                    btnDelete.IsVisible = false;
                    containRow1.IsVisible = true;
                }
                mealPlanViewModel.userMeal.dinnerRecipe = null;
            }
        }

        private async void resetDinner_Invoked(object sender, EventArgs e)
        {
            if (!mealPlanViewModel.IsLoadingPopupRunning)
            {
                mealPlanViewModel.IsLoadingPopupRunning = true;
                mealPlanViewModel.showLoadingPopup();

                UserMealPlanItem userMealPlanItem = new UserMealPlanItem();
                userMealPlanItem.userId = App.LoginViewModel.GoogleUser.UID;
                mealPlanViewModel.Dinner.Clear();
                mealPlanViewModel.Dinner = await mealPlanViewModel.GetMealPlanDinner();
                if (mealPlanViewModel.Dinner.Count > 0)
                {
                    foreach (var item in mealPlanViewModel.Dinner)
                    {
                        userMealPlanItem.dinnerRecipe = item._id;
                    }
                }
                bool check = await mealPlanViewModel.AddUserMealPlannerItem(userMealPlanItem);
                if (check)
                {
                    mealPlanViewModel.userMeal = await mealPlanViewModel.GetUserMealPlanItem();
                    mealPlanViewModel.closeLoadindPopup();

                    if (mealPlanViewModel.userMeal.breakfastRecipe != null || mealPlanViewModel.userMeal.lunchRecipe != null || mealPlanViewModel.userMeal.dinnerRecipe != null)
                    {
                        BackgroundColor = Color.FromHex("F5F5F5");
                        row1.Height = 0;
                        row2.Height = new GridLength(1, GridUnitType.Star);
                        btnDelete.IsVisible = true;
                        containRow1.IsVisible = false;
                        if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                        {
                            deleteBreakfast.IsVisible = true;
                            resetBreakfast.IsVisible = true;
                            addBreakfast.IsVisible = false;
                            row1Breakfast.Height = 0;
                            row2Breakfast.Height = new GridLength(1, GridUnitType.Star);
                            imgBreakfast.Source = mealPlanViewModel.userMeal.breakfastRecipe.image;
                            timeBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.readyInMinutes.ToString() + "min";
                            titleBreakfast.Text = mealPlanViewModel.userMeal.breakfastRecipe.title;
                        }
                        else
                        {
                            deleteBreakfast.IsVisible = false;
                            resetBreakfast.IsVisible = false;
                            addBreakfast.IsVisible = true;
                            row1Breakfast.Height = new GridLength(1, GridUnitType.Star);
                            row2Breakfast.Height = 0;
                        }
                        if (mealPlanViewModel.userMeal.lunchRecipe != null)
                        {
                            deleteLunch.IsVisible = true;
                            resetLunch.IsVisible = true;
                            addLunch.IsVisible = false;
                            row1Lunch.Height = 0;
                            row2Lunch.Height = new GridLength(1, GridUnitType.Star);
                            imgLunch.Source = mealPlanViewModel.userMeal.lunchRecipe.image;
                            timeLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.readyInMinutes.ToString() + "min";
                            titleLunch.Text = mealPlanViewModel.userMeal.lunchRecipe.title;
                        }
                        else
                        {
                            deleteLunch.IsVisible = false;
                            resetLunch.IsVisible = false;
                            addLunch.IsVisible = true;
                            row1Lunch.Height = new GridLength(1, GridUnitType.Star);
                            row2Lunch.Height = 0;
                        }
                        if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                        {
                            deletebinner.IsVisible = true;
                            resetDinner.IsVisible = true;
                            addDinner.IsVisible = false;
                            row1Dinner.Height = 0;
                            row2Dinner.Height = new GridLength(1, GridUnitType.Star);
                            imgDinner.Source = mealPlanViewModel.userMeal.dinnerRecipe.image;
                            timeDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.readyInMinutes.ToString() + "min";
                            titleDinner.Text = mealPlanViewModel.userMeal.dinnerRecipe.title;
                        }
                        else
                        {
                            deletebinner.IsVisible = false;
                            resetDinner.IsVisible = false;
                            addDinner.IsVisible = true;
                            row1Dinner.Height = new GridLength(1, GridUnitType.Star);
                            row2Dinner.Height = 0;
                        }
                    }
                    else
                    {
                        BackgroundColor = Color.FromHex("FFFFFF");
                        row1.Height = new GridLength(1, GridUnitType.Star);
                        row2.Height = 0;
                        btnDelete.IsVisible = false;
                        containRow1.IsVisible = true;
                    }
                } else
                {
                    mealPlanViewModel.closeLoadindPopup();
                }
            }
            
        }

        private async void BreakfastToDetail_Tapped(object sender, EventArgs e)
        {
            if (mealPlanViewModel.userMeal != null)
            {
                if (mealPlanViewModel.userMeal.breakfastRecipe != null)
                {
                    Result recipe = mealPlanViewModel.userMeal.breakfastRecipe;
                    recipe.SelectedViewModelIndex = 0;
                    await Navigation.PushAsync(new DetailRecipe(recipe));
                }
            }
        }

        private async void LunchToDetail_Tapped(object sender, EventArgs e)
        {
            if (mealPlanViewModel.userMeal != null)
            {
                if (mealPlanViewModel.userMeal.lunchRecipe != null)
                {
                    Result recipe = mealPlanViewModel.userMeal.lunchRecipe;
                    recipe.SelectedViewModelIndex = 0;
                    await Navigation.PushAsync(new DetailRecipe(recipe));
                }
            }
        }

        private async void DinnerToDetail_Tapped(object sender, EventArgs e)
        {
            
            if (mealPlanViewModel.userMeal != null)
            {
                if (mealPlanViewModel.userMeal.dinnerRecipe != null)
                {
                    Result recipe = mealPlanViewModel.userMeal.dinnerRecipe;
                    recipe.SelectedViewModelIndex = 0;
                    await Navigation.PushAsync(new DetailRecipe(recipe));
                }
            }
            
        }
    }
}