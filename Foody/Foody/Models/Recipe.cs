using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Us
    {
        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unitShort")]
        public string unitShort { get; set; }

        [JsonProperty("unitLong")]
        public string unitLong { get; set; }
    }

    public class Metric
    {
        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unitShort")]
        public string unitShort { get; set; }

        [JsonProperty("unitLong")]
        public string unitLong { get; set; }
    }

    public class Measures
    {
        [JsonProperty("us")]
        public Us us { get; set; }

        [JsonProperty("metric")]
        public Metric metric { get; set; }
    }

    public class ExtendedIngredient
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("aisle")]
        public string aisle { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("consistency")]
        public string consistency { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("nameClean")]
        public string nameClean { get; set; }

        [JsonProperty("original")]
        public string original { get; set; }

        [JsonProperty("originalString")]
        public string originalString { get; set; }

        [JsonProperty("originalName")]
        public string originalName { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }

        [JsonProperty("meta")]
        public List<string> meta { get; set; }

        [JsonProperty("metaInformation")]
        public List<string> metaInformation { get; set; }

        [JsonProperty("measures")]
        public Measures measures { get; set; }
    }

    public class Nutrient
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }

        [JsonProperty("percentOfDailyNeeds")]
        public double percentOfDailyNeeds { get; set; }
    }

    public class Property
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }
    }

    public class Flavonoid
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }
    }

    public class Ingredient
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }

        [JsonProperty("nutrients")]
        public List<Nutrient> nutrients { get; set; }

        [JsonProperty("localizedName")]
        public string localizedName { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }
    }

    public class CaloricBreakdown
    {
        [JsonProperty("percentProtein")]
        public double percentProtein { get; set; }

        [JsonProperty("percentFat")]
        public double percentFat { get; set; }

        [JsonProperty("percentCarbs")]
        public double percentCarbs { get; set; }
    }

    public class WeightPerServing
    {
        [JsonProperty("amount")]
        public int amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }
    }

    public class Nutrition
    {
        [JsonProperty("nutrients")]
        public List<Nutrient> nutrients { get; set; }

        [JsonProperty("properties")]
        public List<Property> properties { get; set; }

        [JsonProperty("flavonoids")]
        public List<Flavonoid> flavonoids { get; set; }

        [JsonProperty("ingredients")]
        public List<Ingredient> ingredients { get; set; }

        [JsonProperty("caloricBreakdown")]
        public CaloricBreakdown caloricBreakdown { get; set; }

        [JsonProperty("weightPerServing")]
        public WeightPerServing weightPerServing { get; set; }
    }

    public class Equipment
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("localizedName")]
        public string localizedName { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }
    }

    public class Length
    {
        [JsonProperty("number")]
        public int number { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }
    }

    public class Step
    {
        [JsonProperty("number")]
        public int number { get; set; }

        [JsonProperty("step")]
        public string step { get; set; }

        [JsonProperty("ingredients")]
        public List<Ingredient> ingredients { get; set; }

        [JsonProperty("equipment")]
        public List<Equipment> equipment { get; set; }

        [JsonProperty("length")]
        public Length length { get; set; }
    }

    public class AnalyzedInstruction
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("steps")]
        public List<Step> steps { get; set; }
    }

    public class MissedIngredient
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }

        [JsonProperty("unitLong")]
        public string unitLong { get; set; }

        [JsonProperty("unitShort")]
        public string unitShort { get; set; }

        [JsonProperty("aisle")]
        public string aisle { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("original")]
        public string original { get; set; }

        [JsonProperty("originalString")]
        public string originalString { get; set; }

        [JsonProperty("originalName")]
        public string originalName { get; set; }

        [JsonProperty("metaInformation")]
        public List<string> metaInformation { get; set; }

        [JsonProperty("meta")]
        public List<string> meta { get; set; }

        [JsonProperty("extendedName")]
        public string extendedName { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }
    }

    public class Result
    {
        [JsonProperty("vegetarian")]
        public bool vegetarian { get; set; }

        [JsonProperty("vegan")]
        public bool vegan { get; set; }

        [JsonProperty("glutenFree")]
        public bool glutenFree { get; set; }

        [JsonProperty("dairyFree")]
        public bool dairyFree { get; set; }

        [JsonProperty("veryHealthy")]
        public bool veryHealthy { get; set; }

        [JsonProperty("cheap")]
        public bool cheap { get; set; }

        [JsonProperty("veryPopular")]
        public bool veryPopular { get; set; }

        [JsonProperty("sustainable")]
        public bool sustainable { get; set; }

        [JsonProperty("weightWatcherSmartPoints")]
        public int weightWatcherSmartPoints { get; set; }

        [JsonProperty("gaps")]
        public string gaps { get; set; }

        [JsonProperty("lowFodmap")]
        public bool lowFodmap { get; set; }

        [JsonProperty("aggregateLikes")]
        public int aggregateLikes { get; set; }

        [JsonProperty("spoonacularScore")]
        public double spoonacularScore { get; set; }

        [JsonProperty("healthScore")]
        public double healthScore { get; set; }

        [JsonProperty("creditsText")]
        public string creditsText { get; set; }

        [JsonProperty("license")]
        public string license { get; set; }

        [JsonProperty("sourceName")]
        public string sourceName { get; set; }

        [JsonProperty("pricePerServing")]
        public double pricePerServing { get; set; }

        [JsonProperty("extendedIngredients")]
        public List<ExtendedIngredient> extendedIngredients { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("readyInMinutes")]
        public int readyInMinutes { get; set; }

        [JsonProperty("servings")]
        public int servings { get; set; }

        [JsonProperty("sourceUrl")]
        public string sourceUrl { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("imageType")]
        public string imageType { get; set; }

        [JsonProperty("nutrition")]
        public Nutrition nutrition { get; set; }

        [JsonProperty("summary")]
        public string summary { get; set; }

        [JsonProperty("cuisines")]
        public List<string> cuisines { get; set; }

        [JsonProperty("dishTypes")]
        public List<string> dishTypes { get; set; }

        [JsonProperty("diets")]
        public List<string> diets { get; set; }

        [JsonProperty("occasions")]
        public List<object> occasions { get; set; }

        [JsonProperty("analyzedInstructions")]
        public List<AnalyzedInstruction> analyzedInstructions { get; set; }

        [JsonProperty("spoonacularSourceUrl")]
        public string spoonacularSourceUrl { get; set; }

        [JsonProperty("usedIngredientCount")]
        public int usedIngredientCount { get; set; }

        [JsonProperty("missedIngredientCount")]
        public int missedIngredientCount { get; set; }

        [JsonProperty("missedIngredients")]
        public List<MissedIngredient> missedIngredients { get; set; }

        [JsonProperty("likes")]
        public int likes { get; set; }

        [JsonProperty("usedIngredients")]
        public List<object> usedIngredients { get; set; }

        [JsonProperty("unusedIngredients")]
        public List<object> unusedIngredients { get; set; }
    }

    public class Recipe
    {
        [JsonProperty("results")]
        public List<Result> results { get; set; }
    }
}
