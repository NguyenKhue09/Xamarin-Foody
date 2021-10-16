using Foody.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Foody.Services.RecipeApiCall
{
    public class RestService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public Recipe Recipes { get; private set; }
        public Recipe RandomRecipes { get; private set; }

        
        public RestService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<Recipe> GetRecipes()
        {
            
            Recipes = new Recipe();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/recipes/complexSearch?number=" +
                $"{Constants.Constants.NUMBER}&apiKey={Constants.Constants.APIKEY}&type={Constants.Constants.RECIPE_TYPE}" +
                $"&diet={Constants.Constants.DIET}&addRecipesInformation={Constants.Constants.ADDRECIPEINFORMATION}" +
                $"&fillIngredients={Constants.Constants.FILLINGREDIENTS}&addRecipeNutrition={Constants.Constants.RECIPENUTRITION}", string.Empty));
            try
            {
           
                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine("CallAPI");
                if (response.IsSuccessStatusCode)
                {

                   
                    string content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);
                   
                } else
                {
                    Debug.WriteLine("Thatbai");
                    Recipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Recipes;
        }

        public async Task<Recipe> GetRandomRecipes()
        {

            Recipes = new Recipe();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/recipes/random?number={Constants.Constants.NUMBER}&apiKey={Constants.Constants.APIKEY}&tags=vegetarian,dessert", string.Empty));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine("CallAPI");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    RandomRecipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    RandomRecipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return RandomRecipes;
        }

    }
}
