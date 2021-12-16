using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    public class UserMealPlanResult
    {
        public string _id { get; set; }
        public string userId { get; set; }
        public Result breakfastRecipe { get; set; }
        public Result dinnerRecipe { get; set; }
        public Result lunchRecipe { get; set; }
    }
}
