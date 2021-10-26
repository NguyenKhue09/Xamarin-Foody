using Foody.AsyncLazyInit;
using Foody.Models.Local;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Data.Local
{
    public class IngredientsDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<IngredientsDatabase> Instance = new AsyncLazy<IngredientsDatabase>(async () =>
            {
                var instance = new IngredientsDatabase();
                CreateTableResult result = await Database.CreateTableAsync<ingredient>();
                return instance;
            }
        );

        public IngredientsDatabase()
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

    }
}
