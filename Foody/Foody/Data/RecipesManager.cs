using Foody.Models;
using Foody.Services.RecipeApiCall;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Data
{
    public class RecipesManager
    {
        RestService restService;

        public RecipesManager(RestService service)
        {
            restService = service;
        }

        public Task<Recipe> GetRecipes()
        {
            return restService.GetRecipes();
        }

        public Task<Recipe> GetPantryRecipes()
        {
            return restService.GetPantryRecipes();
        }

        public Task<Recipe> GetRandomRecipes()
        {
            return restService.GetRandomRecipes();
        }

        public Task<Recipe> GetPopularRecipes()
        {
            return restService.GetPopularRecipes();
        }

        public Task<Recipe> SearchRecipes(string query, string cuisine, string intolerances)
        {
            return restService.SearchRecipes(query, cuisine, intolerances);
        }


        // Shopping list api
        public Task<bool> AddIngredientsToShoppingList(List<ItemShoppingList> itemShoppingLists)
        {
            return restService.AddIngredientsToShoppingList(itemShoppingLists);
        }

        public Task<ShoppingListResult> GetShoppingList()
        {
            return restService.GetShoppingList();
        }

        public Task<bool> DeleteShoppingListItem(string itemId)
        {
            return restService.DeleteShoppingListItem(itemId);
        }

        public Task<bool> DeleteManyShoppingListItem(List<string> listId)
        {
            return restService.DeleteManyShoppingListItem(listId);
        }
        //shopping cart api
        public Task<bool> AddIngredientsToShoppingCart(List<ItemShoppingCart> itemShoppingCarts)
        {
            return restService.AddIngredientsToShoppingCart(itemShoppingCarts);
        }

        public Task<ShoppingCartResult> GetShoppingCart()
        {
            return restService.GetShoppingCart();
        }

        public Task<bool> DeleteShoppingCart(string itemId)
        {
            return restService.DeleteShoppingCart(itemId);
        }

        public Task<bool> AddShoppingCartToUserPantry()
        {
            return restService.AddShoppingCartToUserPantry();
        }
        // Ingredient
        //public Task<IngredientInform> GetIngredientInform(int id)
        //{
        //    return restService.GetIngredientInform(id);
        //}

        public Task<SearchIngredientsResult> SearchIngredients(string searchString)
        {
            return restService.SearchIngredients(searchString);
        }

        // PantryBuilder Api
        public Task<PantryBuilderResult> GetPantrybuilderList()
        {
            return restService.GetPantrybuilderList();
        }

        public Task<PantryBuilderResult> SearchPantryBuilder(string searchString)
        {
            return restService.SearchPantryBuilder(searchString);
        }

        // Pantry api
        public Task<bool> AddItemToUserPantry(UserPantryItem userPantryItem)
        {
            return restService.AddItemToUserPantry(userPantryItem);
        }

        public Task<UserPantryItemResult> GetUserPantryItems(string userId)
        {
            return restService.GetUserPantryItems(userId);
        }

        public Task<bool> DeleteUserPantryItem(string itemId, string userId)
        {
            return restService.DeleteUserPantryItem(itemId, userId);
        }

        public Task<bool> DeleteManyUserPantryItem(List<string> listId)
        {
            return restService.DeleteManyUserPantryItem(listId);
        }
        public Task<bool> DeleteAllUserPantryItem(string userId)
        {
            return restService.DeleteAllUserPantryItem(userId);
        }

        // User MealPlanner Api
        public Task<bool> AddUserMealPlannerItem(UserMealPlanItem userMealPlanItem)
        {
            return restService.AddUserMealPlannerItem(userMealPlanItem);
        }

        public Task<UserMealPlanResult> GetUserMealPlanItem()
        {
            return restService.GetUserMealPlanItem();
        }

        // get api meal plan type
        public Task<Recipe> GetMealPlanBreakfast()
        {
            return restService.GetMealPlanBreakfast();
        }

        public Task<Recipe> GetMealPlanLunch()
        {
            return restService.GetMealPlanLunch();
        }

        public Task<Recipe> GetMealPlanDinner()
        {
            return restService.GetMealPlanDinner();
        }

        public Task<bool> DeleteUserMealPlanItem(string type)
        {
            return restService.DeleteUserMealPlanItem(type);
        }
    }
}
