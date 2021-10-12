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

        public Task<List<Recipe>> GetRecipes()
        {
            return restService.GetRecipes();
        }
    }
}
