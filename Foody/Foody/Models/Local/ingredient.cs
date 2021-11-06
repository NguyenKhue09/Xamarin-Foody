using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models.Local
{
    public class ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string aisleBelong { get; set; }
        public string ingredientName { get; set; }

        public string ingredientImg { get; set; }
    }
}
