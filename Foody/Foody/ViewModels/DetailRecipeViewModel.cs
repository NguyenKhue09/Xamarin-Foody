using Foody.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class DetailRecipeViewModel : BaseViewModel
    {

        public INavigation Navigation;
        public Result recipe { get; set; }
        public List<Nutrient> newNutrients { get; set; }
        public ObservableCollection<ExtendedIngredient> extendedIngredients { get; set; }

       

        public int numberOfIngredient;
       
        public Command sub { get; }
        public Command plus { get; }


        public int NumberOfIngredient
        {
            get { return numberOfIngredient; }
            set { SetProperty(ref numberOfIngredient, value); }
        }

        


        public Rectangle rect { get; set; }

        List<ExtendedIngredient> newList; 

        public DetailRecipeViewModel(Result result)
        { 
            recipe = result;
            rect = new Rectangle(0, 0, 0.5, 1);
            newNutrients = setProgresBarValue(recipe.nutrition.nutrients);
            numberOfIngredient = 1;
            newList = jsonCloneObject(recipe.extendedIngredients);
            extendedIngredients = new ObservableCollection<ExtendedIngredient>(newList);
            changeAmountIngredients(numberOfIngredient);
            sub = new Command(() => SubCount());
            plus = new Command(() => PlusCount());
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

        public void SubCount()
        {
            if (numberOfIngredient > 0)
            {
                NumberOfIngredient -= 1;
                changeAmountIngredients(numberOfIngredient);
            }
        }

        public void PlusCount()
        {
   
            NumberOfIngredient += 1;
            Debug.WriteLine(NumberOfIngredient);
            changeAmountIngredients(NumberOfIngredient);
        }
        
        public void changeAmountIngredients(int amount)
        {
            Debug.WriteLine(amount);
            for (int i = 0; i < newList.Count; i++)
            {

                extendedIngredients[i] = newList[i];
                extendedIngredients[i].amount = double.Parse(recipe.extendedIngredients[i].amount.ToString()) * amount;
                Debug.WriteLine(extendedIngredients[0].amount);

            }
            
        }

        public static T jsonCloneObject<T>(T source)
        {
            string json = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
