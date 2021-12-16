using Foody.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Foody.ViewModels
{
    public class MealPlanViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Result> Breakfast { get; set; }
        public ObservableRangeCollection<Result> Lunch { get; set; }
        public ObservableRangeCollection<Result> Dinner { get; set; }
        public MealPlanViewModel()
        {
            Breakfast = new ObservableRangeCollection<Result>();
            Lunch = new ObservableRangeCollection<Result>();
            Dinner = new ObservableRangeCollection<Result>();
        }
        async public Task<ObservableRangeCollection<Result>> GetMealPlanBreakfast()
        {
            ObservableRangeCollection<Result> result = new ObservableRangeCollection<Result>();
            Recipe Results = await App.RecipeManager.GetMealPlanBreakfast();
            if (Results != null)
            {
                Debug.WriteLine("Breakfast call api success");
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
                Debug.WriteLine("Breakfast call api success");
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
                Debug.WriteLine("Breakfast call api success");
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
    }
}
