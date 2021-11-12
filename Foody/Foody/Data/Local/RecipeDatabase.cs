using Foody.AsyncLazyInit;
using Foody.Models.Local;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Data.Local
{
    public class RecipeDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<RecipeDatabase> Instance = new AsyncLazy<RecipeDatabase>(async () =>
            {
                var instance = new RecipeDatabase();
                CreateTableResult IngredientTable = await Database.CreateTableAsync<ingredient>();
                CreateTableResult FavoriteReipeTable = await Database.CreateTableAsync<FavoriteRecipe>();
                return instance;
            }
        );

        public RecipeDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.Constants.DatabasePath, Constants.Constants.Flags);
        }
         
        public Task<List<ingredient>> GetIngredientAsync()
        {
            return Database.Table<ingredient>().ToListAsync();
        }

        public Task<List<ingredient>> GetIngredientsByAisle(string aisle)
        {
            return Database.QueryAsync<ingredient>($"SELECT * FROM [ingredient] WHERE [aisleBelong] = {aisle}");
        }

        public Task<ingredient> GetIngredientById(int id)
        {
            return Database.Table<ingredient>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveIngredientAsync(ingredient ingredient)
        {
            if(ingredient.ID != 0)
            {
                return Database.UpdateAsync(ingredient);
            } else
            {
                return Database.InsertAsync(ingredient);
            }
        }

        public Task<int> DeleteIngredientAsync(ingredient ingredient)
        {
            return Database.DeleteAsync(ingredient);
        }

        // Favorite Recipe
        //public Task<List<FavoriteRecipe>> GetFavoriteRecipes()
        //{
           
        //    return null;
        //}

        //public Task<int> AddFavoriteRecipe()
        //{
        //    Debug.WriteLine("Add favorite recipe");
        //    return 1;
        //}

        //public Task<int> DeleteFavoriteRecipe()
        //{
        //    Debug.WriteLine("Delete favorite recipe");
        //    return 1;
        //}

    }
}
