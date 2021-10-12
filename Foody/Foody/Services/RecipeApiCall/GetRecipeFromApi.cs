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

        public List<Recipe> Recipes { get; private set; }

        
        public RestService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Recipe>> GetRecipes()
        {
            Recipes = new List<Recipe>();

            Uri uri = new Uri(string.Format($"{Constants.Constants.BASEURL}/recipes/complexSearch?{Constants.Constants.NUMBER}&{Constants.Constants.APIKEY}&{Constants.Constants.RECIPE_TYPE}&{Constants.Constants.DIET}&{Constants.Constants.ADDRECIPEINFORMATION}&{Constants.Constants.FILLINGREDIENTS}&{Constants.Constants.RECIPENUTRITION}", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonSerializer.Deserialize<List<Recipe>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Recipes;
        }

    }
}
