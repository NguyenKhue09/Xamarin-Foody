using Foody.Models;
using Foody.Views.PopUp;
using MvvmHelpers;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class MealPlanViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Result> Breakfast { get; set; }
        public ObservableRangeCollection<Result> Lunch { get; set; }
        public ObservableRangeCollection<Result> Dinner { get; set; }
        public UserMealPlanResult userMeal { get; set; }

        public bool isMealPlanPopupRunning = false;
        public bool isLoadingPopupRunning = false;
        public bool IsMealPlanPopupRunning
        {
            get { return isMealPlanPopupRunning; }
            set { SetProperty(ref isMealPlanPopupRunning, value); }
        }

        public bool IsLoadingPopupRunning
        {
            get { return isLoadingPopupRunning; }
            set { SetProperty(ref isLoadingPopupRunning, value); }
        }

        readonly INavigation Navigation;

        private MealPlanPopup mealPlanPopup { get; set; }
        private LoadingPopup loadingPopup { get; set; }
        public MealPlanViewModel()
        {
            Breakfast = new ObservableRangeCollection<Result>();
            Lunch = new ObservableRangeCollection<Result>();
            Dinner = new ObservableRangeCollection<Result>();
            userMeal = new UserMealPlanResult();
            mealPlanPopup = new MealPlanPopup();
            loadingPopup = new LoadingPopup();
        }

        public async void showMealPlanPopup()
        {
            await Navigation.PushPopupAsync(new MealPlanPopup());
            //IsMealPlanPopupRunning = true;
        }

        public async void showLoadingPopup()
        {
            await Navigation.PushPopupAsync(new LoadingPopup());
        }

        public async void closeMealPlanPopup()
        {
            await mealPlanPopup.closeMealPlanPopup();
            IsMealPlanPopupRunning = false;
        }

        public async void closeLoadindPopup()
        {
            await loadingPopup.closeLoadingPopup();
            IsLoadingPopupRunning = false;
        }

        async public Task<ObservableRangeCollection<Result>> GetMealPlanBreakfast()
        {
            ObservableRangeCollection<Result> result = new ObservableRangeCollection<Result>();
            Recipe Results = await App.RecipeManager.GetMealPlanBreakfast();
            if (Results != null)
            {
                result.AddRange(Results.results);
            }
            return result;
        }

        async public Task<ObservableRangeCollection<Result>> GetMealPlanLunch()
        {
            ObservableRangeCollection<Result> result = new ObservableRangeCollection<Result>();
            Recipe Results = await App.RecipeManager.GetMealPlanLunch();
            if (Results != null)
            {
                result.AddRange(Results.results);
            }
            return result;
        }

        async public Task<ObservableRangeCollection<Result>> GetMealPlanDinner()
        {
            ObservableRangeCollection<Result> result = new ObservableRangeCollection<Result>();
            Recipe Results = await App.RecipeManager.GetMealPlanDinner();
            if (Results != null)
            {
                result.AddRange(Results.results);
            }
            return result;
        }

        async public Task<bool> AddUserMealPlannerItem(UserMealPlanItem userMealPlanItem)
        {
            bool result = await App.RecipeManager.AddUserMealPlannerItem(userMealPlanItem);
            return result;
        }

        public async Task<UserMealPlanResult> GetUserMealPlanItem()
        {
            UserMealPlanResult userMealPlan = new UserMealPlanResult();
            UserMealPlanResult Results = await App.RecipeManager.GetUserMealPlanItem();
            if (Results != null)
            {
                userMealPlan = Results;
            }
            return userMealPlan;
        }

        public async Task<bool> DeleteUserMealPlanItem(string type)
        {
            bool result = await App.RecipeManager.DeleteUserMealPlanItem(type);
            return result;
        }
    }
}
