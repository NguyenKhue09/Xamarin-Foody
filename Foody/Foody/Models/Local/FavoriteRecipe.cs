using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models.Local
{
    public class FavoriteRecipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string userId { get; set; }
        public int RecipeId { get; set; }

        public string JsonRecipe { get; set; }

    }
}
