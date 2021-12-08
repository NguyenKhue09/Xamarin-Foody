using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Foody.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
  
    public class ExtendedIngredient
    {
        public int id { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string amountIngre { get; set; }
        public string unit { get; set; }
    }


    public class Nutrient
    {
        public string name { get; set; }
        public string title { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public double percentOfDailyNeeds { get; set; }
        public Rectangle rectangle { get; set; }
        public string color { get; set; }
    }

    public class Nutrition
    {
        public List<Nutrient> nutrients { get; set; }
    }

    public class Step
    {
        public int number { get; set; }
        public string step { get; set; }
    }

    public class AnalyzedInstruction
    {
        public string name { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Result
    {
        public bool vegetarian { get; set; }
        public bool vegan { get; set; }
        public List<ExtendedIngredient> extendedIngredients { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public List<string> cuisines { get; set; }
        public List<string> dishTypes { get; set; }
        public List<string> diets { get; set; }
        public Nutrition nutrition { get; set; }
        public List<AnalyzedInstruction> analyzedInstructions { get; set; }
        public int SelectedViewModelIndex { get; set; }
    }

    public class Recipe
    {
        public List<Result> results { get; set; }
    }


}
