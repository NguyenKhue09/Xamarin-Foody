using Foody.Models;
using Foody.ViewModels;
using Foody.Views.PopUp;
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
        private PantryViewModel pantryViewModel = new PantryViewModel();
        private SearchPopUp searchPopUp = new SearchPopUp();

        public Recipe Recipes { get; private set; }
        public Recipe RandomRecipes { get; private set; }
        public Recipe PopularRecipes { get; private set; }
        public Recipe SearchRecipesList { get; private set; }


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
        // popular Recipe 
        public async Task<Recipe> GetPopularRecipes()
        {

            PopularRecipes = new Recipe();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/recipes/complexSearch?number=" +
                $"{Constants.Constants.NUMBER}&apiKey={Constants.Constants.APIKEY}&type={Constants.Constants.POPULAR_RECIPE_TYPE}" +
                $"&diet={Constants.Constants.POPULAR_DIET}&addRecipesInformation={Constants.Constants.ADDRECIPEINFORMATION}" +
                $"&fillIngredients={Constants.Constants.FILLINGREDIENTS}&addRecipeNutrition={Constants.Constants.RECIPENUTRITION}", string.Empty));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine("CallAPI");
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    PopularRecipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    PopularRecipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PopularRecipes;
        }

        public async Task<Recipe> SearchRecipes(string query, string cuisine, string intolerances)
        {

            SearchRecipesList = new Recipe();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/recipes/complexSearch?number=" +
                $"{Constants.Constants.NUMBER}&apiKey={Constants.Constants.APIKEY}&type={Constants.Constants.POPULAR_RECIPE_TYPE}" +
                $"&diet={Constants.Constants.POPULAR_DIET}&addRecipesInformation={Constants.Constants.ADDRECIPEINFORMATION}" +
                $"&fillIngredients={Constants.Constants.FILLINGREDIENTS}&addRecipeNutrition={Constants.Constants.RECIPENUTRITION}" +
                $"&query={query}&cuisine={cuisine}&intolerances={intolerances}", string.Empty));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine("CallAPI");
                await pantryViewModel.showpopup_Clicked();
                if (response.IsSuccessStatusCode)
                {

                    
                    string content = await response.Content.ReadAsStringAsync();
                    SearchRecipesList = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                    await searchPopUp.closePopup();
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    SearchRecipesList.results = new List<Result>();
                    await searchPopUp.closePopup();

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return SearchRecipesList;
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
