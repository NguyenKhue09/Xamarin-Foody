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
        public ShoppingCartResult ShoppingCartResult { get; private set; }
        public IngredientInform ingredientInform { get; private set; }
        public PantryBuilderResult PantryBuilderResult { get; private set; }
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
            Recipe Recipes = new Recipe();
            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/recipes/get-recipe"));
            try
            {
         
                HttpResponseMessage response = await client.GetAsync(uri);
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
                Recipes.results = new List<Result>();
            }

            return Recipes;
        }

        // pantry recipes
        public async Task<Recipe> GetPantryRecipes()
        {
            
            Recipe Recipes = new Recipe();

            string userId = App.LoginViewModel.GoogleUser.UID;

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/recipes/get-recipe-by-user-pantry/{userId}"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    Recipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Recipes.results = new List<Result>();
            }

            return Recipes;
        }

        // popular Recipe 
        public async Task<Recipe> GetPopularRecipes()
        {
            Recipe PopularRecipes  = new Recipe();
            string[] Diet = { "ketogenic", "gluten free", "vegan", "primal" };
            Random rnd = new Random();
            int index = rnd.Next(Diet.Length);
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/recipes/search-recipe?diet={Diet[index]}&type=lunch"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
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
                PopularRecipes.results = new List<Result>();
            }

            return PopularRecipes;
        }

        public async Task<Recipe> SearchRecipes(string query, string cuisine, string intolerances)
        {

            SearchRecipesList = new Recipe();

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/recipes/search-recipe-pantry?query={query}&cuisine={cuisine}&intolerances={intolerances}", string.Empty));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                await pantryViewModel.showpopup_Clicked();
                if (response.IsSuccessStatusCode)
                {
                  
                    string content = await response.Content.ReadAsStringAsync();
                    SearchRecipesList = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                    await searchPopUp.closeSearchPopup();
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    SearchRecipesList.results = new List<Result>();
                    await searchPopUp.closeSearchPopup();

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                SearchRecipesList.results = new List<Result>();
                await searchPopUp.closeSearchPopup();
            }

            return SearchRecipesList;
        }

        public async Task<Recipe> GetRandomRecipes()
        {

            string[] Diet = { "ketogenic", "gluten free", "vegan", "primal" };
            Random rnd = new Random();
            int index = rnd.Next(Diet.Length);
            string[] Type = { "main course", "side dish", "dessert", "appetizer", "salad", "bread", "breakfast", "soup", "beverage", "sauce", "marinade", "fingerfood", "snack", "drink" };
            Random random = new Random();
            int index1 = random.Next(Type.Length);
            Recipe RandomRecipes = new Recipe();

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/recipes/search-recipe?diet={Diet[index]}&type={Type[index1]}"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
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
                RandomRecipes.results = new List<Result>();
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return RandomRecipes;
        }

        // Shopping list api
        public async Task<bool> AddIngredientsToShoppingList(List<ItemShoppingList> itemShoppingLists)
        {
           
            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/shopping-list/add-shopping-list-item"));


            try
            {
                string json = JsonSerializer.Serialize(itemShoppingLists, serializerOptions);
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
            string userId = App.LoginViewModel.GoogleUser.UID;

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/shopping-list/get-shopping-list-item/{userId}"));

            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
               
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    ShoppingListResult = JsonSerializer.Deserialize<ShoppingListResult>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    ShoppingListResult.results = new List<Item>();
                    await searchPopUp.closeSearchPopup();

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                ShoppingListResult.results = new List<Item>();
            }

            return ShoppingListResult;
        }

        public async Task<bool> DeleteShoppingListItem(string itemId)
        {
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/shopping-list/delete-shopping-list-item/{itemId}"));

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

        public async Task<bool> DeleteManyShoppingListItem(List<string> listId)
        {
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/shopping-list/delete-many-shopping-list-item"));

            try
            {
                string json = JsonSerializer.Serialize(listId, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("ThanhCong");
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        // Shopping cart api
        public async Task<bool> AddIngredientsToShoppingCart(ItemShoppingCart itemShoppingCart)
        {
            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/shopping-cart/add-shopping-cart-item"));


            try
            {
                string json = JsonSerializer.Serialize(itemShoppingCart, serializerOptions);
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
        public async Task<ShoppingCartResult> GetShoppingCart()
        {
            string ueserID = App.LoginViewModel.ObsGoogleUser.UID;
            
            ShoppingCartResult = new ShoppingCartResult();

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/shopping-cart/get-shopping-cart-item/{ueserID}"));
           
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ShoppingCartResult = JsonSerializer.Deserialize<ShoppingCartResult>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    ShoppingCartResult.resultsCart = new List<ItemCart>();
                    Debug.WriteLine("Thatbai");

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ShoppingCartResult;
        }

        public async Task<bool> DeleteShoppingCart(string itemId)
        {
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/shopping-cart/delete-shopping-cart-item/{itemId}"));
            
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

        public async Task<bool> AddShoppingCartToUserPantry()
        {
            string ueserID = App.LoginViewModel.ObsGoogleUser.UID;

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/shopping-cart/add-shopping-cart-item-to-user-pantry/{ueserID}"));

            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

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

        // Ingredient
        //public async Task<IngredientInform> GetIngredientInform(int id)
        //{
        //    Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/food/ingredients/{id}/information?apiKey={Constants.Constants.APIKEY}&amount=1"));

        //    ingredientInform = new IngredientInform();

        //    try 
        //    {

        //        HttpResponseMessage response = await client.GetAsync(uri);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            ingredientInform = JsonSerializer.Deserialize<IngredientInform>(content, serializerOptions);
        //            Debug.WriteLine("ThanhCong");
        //        }
        //        else
        //        {
        //            Debug.WriteLine("Thatbai");
        //            ingredientInform = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }

        //    return ingredientInform;
        //}

        public async Task<SearchIngredientsResult> SearchIngredients(string searchString)
        {
            SearchIngredientsResult results = new SearchIngredientsResult();

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/ingredient/search-ingredient?query={searchString}"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    results = JsonSerializer.Deserialize<SearchIngredientsResult>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    results = null;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return results;
        }

        // Pantry Builder Api
        public async Task<PantryBuilderResult> GetPantrybuilderList()
        {

            PantryBuilderResult = new PantryBuilderResult();

            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/pantry-builder/get-pantry-builder"));

            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {

                    string content = await response.Content.ReadAsStringAsync();
                    PantryBuilderResult = JsonSerializer.Deserialize<PantryBuilderResult>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    PantryBuilderResult.pantryBuilder = new List<PantryBuilder>();

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PantryBuilderResult;
        }

        public async Task<PantryBuilderResult> SearchPantryBuilder(string searchString)
        {
            PantryBuilderResult results = new PantryBuilderResult();

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/pantry-builder/search-pantry-builder?query={searchString}"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    results = JsonSerializer.Deserialize<PantryBuilderResult>(content, serializerOptions);
                    Debug.WriteLine("ThanhCong");
                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    results = null;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return results;
        }

        // Pantry api
        public async Task<bool> AddItemToUserPantry(UserPantryItem userPantryItem)
        {
            
            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/pantry/add-user-pantry-item"));

            try
            {
                string json = JsonSerializer.Serialize(userPantryItem, serializerOptions);
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

        public async Task<UserPantryItemResult> GetUserPantryItems(string userId)
        {
            UserPantryItemResult userPantryItemResult = new UserPantryItemResult();

            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/pantry/get-user-pantry-item/{userId}"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    userPantryItemResult = JsonSerializer.Deserialize<UserPantryItemResult>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    userPantryItemResult = null;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                userPantryItemResult = null;
            }

            return userPantryItemResult;
        }

        public async Task<bool> DeleteUserPantryItem(string itemId, string userId)
        {
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/pantry/delete-user-pantry-item/{userId}/{itemId}"));

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

        public async Task<bool> DeleteAllUserPantryItem(string userId)
        {
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/pantry/delete-all-user-pantry-item/{userId}"));

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

        // User Meal Planner Api
        public async Task<bool> AddUserMealPlannerItem(UserMealPlanItem userMealPlanItem)
        {
            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/meal-plan/add-user-meal-plan"));

            try
            {
                string json = JsonSerializer.Serialize(userMealPlanItem, serializerOptions);
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

        public async Task<UserMealPlanResult> GetUserMealPlanItem()
        {
            UserMealPlanResult userMealPlanResult = new UserMealPlanResult();
            string userId = App.LoginViewModel.GoogleUser.UID;
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/meal-plan/get-user-meal-plan/{userId}"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    userMealPlanResult = JsonSerializer.Deserialize<UserMealPlanResult>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    userMealPlanResult = null;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                userMealPlanResult = null;
            }

            return userMealPlanResult;
        }
        // get meal planner type
        public async Task<Recipe> GetMealPlanLunch()
        {
            Recipe Recipes = new Recipe();

            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/recipes/get-meal-recipe?type=lunch"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    Recipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Recipes.results = new List<Result>();
            }

            return Recipes;
        }

        public async Task<Recipe> GetMealPlanDinner()
        {
            Recipe Recipes = new Recipe();

            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/recipes/get-meal-recipe?type=dinner"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    Recipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Recipes.results = new List<Result>();
            }

            return Recipes;
        }

        public async Task<Recipe> GetMealPlanBreakfast()
        {
            Recipe Recipes = new Recipe();

            Uri uri = new Uri(string.Format("https://pantry-wizard.herokuapp.com/api/recipes/get-meal-recipe?type=breakfast"));
            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {


                    string content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonSerializer.Deserialize<Recipe>(content, serializerOptions);

                }
                else
                {
                    Debug.WriteLine("Thatbai");
                    Recipes.results = new List<Result>();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Recipes.results = new List<Result>();
            }

            return Recipes;
        }

        public async Task<bool> DeleteUserMealPlanItem(string type)
        {
            string userId = App.LoginViewModel.GoogleUser.UID;
            Uri uri = new Uri(string.Format($"https://pantry-wizard.herokuapp.com/api/meal-plan/delete-user-meal-plan/{userId}/{type}"));

            try
            {

                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("ThanhCong");
                    return true;
                }
                else
                {
                    Debug.WriteLine("That bai");
                    return false;

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }
    }
}
