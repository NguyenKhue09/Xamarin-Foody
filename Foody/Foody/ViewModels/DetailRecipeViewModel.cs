using Foody.Converters;
using Foody.Models;
using Foody.Views.PopUp;
using MvvmHelpers;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
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

        private LoadingPopup loadingPopUp = new LoadingPopup();

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

        async public Task showLoadingPopup_Clicked()
        {
            await Navigation.PushPopupAsync(new LoadingPopup());
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
            changeAmountIngredients(NumberOfIngredient);
        }
        
        public void changeAmountIngredients(int amount)
        {
            for (int i = 0; i < newList.Count; i++)
            {
                newList[i].amount = recipe.extendedIngredients[i].amount * amount;
                //Debug.WriteLine($"amount {newList[i].amount}");
                //Debug.WriteLine($"Rounded amount {Math.Round(newList[i].amount, 2)}");
                newList[i].amountIngre = new Fraction(Math.Round(newList[i].amount, 2)).ToString();
                //newList[i].amountIngre = Math.Round(newList[i].amount, 2).ToString();
                ExtendedIngredients[i] = newList[i];
            }
            
        }


        public async Task<bool> AddIngredientsToShoppingList()
        {
            List<ItemShoppingList> itemShoppingLists = new List<ItemShoppingList>();
            foreach (ExtendedIngredient item in ExtendedIngredients)
            {
                
                ItemShoppingList itemShoppingList = new ItemShoppingList
                {
                    id = item.id,
                    aisle = item.aisle,
                    name = item.name,
                    amount = item.amount,
                    unit = item.unit,
                    image = item.image,
                    userId = App.LoginViewModel.GoogleUser.UID
                };

                itemShoppingLists.Add(itemShoppingList);
            }

            bool response = await App.RecipeManager.AddIngredientsToShoppingList(itemShoppingLists);
            if (!response)
            {
                await loadingPopUp.closeLoadingPopup();
                return false;
            } else
            {
                await loadingPopUp.closeLoadingPopup();
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
