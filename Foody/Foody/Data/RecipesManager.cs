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

        public Task<Recipe> GetPopularRecipes()
        {
            return restService.GetPopularRecipes();
        }

        public Task<Recipe> SearchRecipes(string query, string cuisine, string intolerances)
        {
            return restService.SearchRecipes(query, cuisine, intolerances);
        }


        // Shopping list api
        public Task<bool> AddIngredientsToShoppingList(ItemShoppingList iemShoppingList)
        {
            return restService.AddIngredientsToShoppingList(iemShoppingList);
        }
         
        public Task<ShoppingListResult> GetShoppingList()
        {
            return restService.GetShoppingList();
        }

        public Task DeleteShoppingListItem(string itemId)
        {
            return restService.DeleteShoppingListItem(itemId);
        }

        public Task<IngredientInform> GetIngredientInform(int id)
        {
            return restService.GetIngredientInform(id);
        }

    }
}
