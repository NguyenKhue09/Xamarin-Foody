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
       
        public Recipe PopularRecipes { get; private set; }
        public Recipe SearchRecipesList { get; private set; }
        public ShoppingListResult ShoppingListResult { get; private set; }
        public IngredientInform ingredientInform { get; private set; }


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
            Debug.WriteLine("Recipe");
            Recipe Recipes = new Recipe();

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
            Debug.WriteLine("Popular");
            Recipe PopularRecipes  = new Recipe();

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

            string[] names = { "Ketogenic", "Gluten Free", "Vegetarian", "Lacto-Vegetarian", "Vegan", "Paleo", "Pescetarian", "Low FODMAP" };
            Random rnd = new Random();
            int index = rnd.Next(names.Length);

            Recipe RandomRecipes = new Recipe();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/recipes/complexSearch?number=" +
               $"{Constants.Constants.NUMBER}&apiKey={Constants.Constants.APIKEY}&type={Constants.Constants.RANDOM_RECIPE_TYPE}" +
               $"&diet={names[index]}&addRecipesInformation={Constants.Constants.ADDRECIPEINFORMATION}" +
               $"&fillIngredients={Constants.Constants.FILLINGREDIENTS}&addRecipeNutrition={Constants.Constants.RECIPENUTRITION}", string.Empty));
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

        // Shopping list api
        public async Task<bool> AddIngredientsToShoppingList(ItemShoppingList itemShoppingList)
        {
            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/mealplanner/{Constants.Constants.USER_NAME}/shopping-list/items?" +
                $"apiKey={Constants.Constants.APIKEY}&hash={Constants.Constants.HASH_USERNAME}"));


            try
            {
                string json = JsonSerializer.Serialize<ItemShoppingList>(itemShoppingList, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
               
                if (response.IsSuccessStatusCode)
                {                   
                    Debug.WriteLine("ThanhCong");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    return false;
                    
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<ShoppingListResult> GetShoppingList()
        {

            ShoppingListResult = new ShoppingListResult();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/mealplanner/{Constants.Constants.USER_NAME}/shopping-list?" +
               $"apiKey={Constants.Constants.APIKEY}&hash={Constants.Constants.HASH_USERNAME}"));

            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine("CallAPI");
               
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    ShoppingListResult = JsonSerializer.Deserialize<ShoppingListResult>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    ShoppingListResult.aisles = new List<Aisle>();
                    await searchPopUp.closePopup();

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ShoppingListResult;
        }


        public async Task<bool> DeleteShoppingListItem(int itemId)
        {
            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/mealplanner/{Constants.Constants.USER_NAME}/shopping-list/items/{itemId}?" +
               $"apiKey={Constants.Constants.APIKEY}&hash={Constants.Constants.HASH_USERNAME}"));


            try
            {

                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("ThanhCong");
                    Debug.WriteLine(response.RequestMessage);
                    return true;
                }
                else
                {
                    Debug.WriteLine(response.RequestMessage);
                    return false;

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<IngredientInform> GetIngredientInform(int id)
        {
            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/food/ingredients/{id}/information?apiKey={Constants.Constants.APIKEY}"));

            ingredientInform = new IngredientInform();

            try 
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ingredientInform = JsonSerializer.Deserialize<IngredientInform>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    ingredientInform = null;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ingredientInform;
        }

    }
}
