using Foody.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class DetailRecipeViewModel: BaseViewModel
    {

        public INavigation Navigation;
        public Result recipe { get; set; }
        public List<Nutrient> newNutrients { get; set; }

       
        public Rectangle rect { get; set; }

        

        public DetailRecipeViewModel(Result result)
        { 
            recipe = result;
            rect = new Rectangle(0, 0, 0.5, 1);
            newNutrients = setProgresBarValue(recipe.nutrition.nutrients);

        }

        public List<Nutrient> setProgresBarValue(List<Nutrient> nutrients)
        {
            
            List<Nutrient> results = new List<Nutrient>();

            for(int i = 0; i < nutrients.Count; i++)
            {
                nutrients[i].rectangle = new Rectangle(0, 0, nutrients[i].percentOfDailyNeeds / 100, 1);
                if(nutrients[i].percentOfDailyNeeds > 100)
                {
                    nutrients[i].color = "#F64136";
                } else
                {
                    nutrients[i].color = "#56E391";
                }
                results.Add(nutrients[i]);

            }
            return results;
        }

    }
}
