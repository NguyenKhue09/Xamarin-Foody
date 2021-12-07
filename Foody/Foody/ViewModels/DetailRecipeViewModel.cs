using Foody.Converters;
using Foody.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class DetailRecipeViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChange;

        public INavigation Navigation;
        public Result recipe { get; set; }
        public List<Nutrient> newNutrients { get; set; }
        public ObservableCollection<ExtendedIngredient> extendedIngredients { get; set; }

        public ObservableCollection<ExtendedIngredient> ExtendedIngredients
        {
            get { return extendedIngredients; }
            set
            {
                extendedIngredients = value;
                NotifyPropertyChanged();
            }
        }


        public int numberOfIngredient;
        public bool isFavoriteRecipe;
       
        public Command sub { get; }
        public Command plus { get; }


        public int NumberOfIngredient
        {
            get { return numberOfIngredient; }
            set { SetProperty(ref numberOfIngredient, value); }
        }

        public bool IsFavoriteRecipe
        {
            get { return isFavoriteRecipe; }
            set { SetProperty(ref isFavoriteRecipe, value); }
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
            IsFavoriteRecipe = false;

        }


        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChange?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            if (numberOfIngredient > 1)
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
            for (int i = 0; i < newList.Count; i++)
            {
                newList[i].amount = double.Parse(recipe.extendedIngredients[i].amount.ToString()) * amount;
                newList[i].amountIngre = new Fraction(newList[i].amount).ToString();
                ExtendedIngredients[i] = newList[i];
            }
            
        }


        public async Task<bool> AddIngredientsToShoppingList()
        {
            Debug.WriteLine("Call function add to list");
            foreach (ExtendedIngredient item in ExtendedIngredients)
            {
                string result = item.amount.ToString() + " " + item.unit + " " + item.name ;
                ItemShoppingList itemShoppingList = new ItemShoppingList
                {
                    aisle = item.aisle,
                    item = result
                };

                bool response = await App.RecipeManager.AddIngredientsToShoppingList(itemShoppingList);
                if (!response) return false;

            }
            return true;
        }

        public void getIngredient()
        {
            foreach(ExtendedIngredient extendedIngredient in recipe.extendedIngredients)
            {
                Debug.WriteLine(extendedIngredient.id);
                Debug.WriteLine(extendedIngredient.name);
                Debug.WriteLine(extendedIngredient.aisle);
                Debug.WriteLine(extendedIngredient.image);
            }
        }
        public static T jsonCloneObject<T>(T source)
        {
            string json = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
