using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models.Local
{
    public class CartIngredient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ingredientId { get; set; }

        public string userID { get; set;}
        public string aisleBelong { get; set; }
        public string ingredientName { get; set; }

        public string ingredientImg { get; set; }
        public string ingredientUnits { get; set; }

        public double amount { get; set; }
        public bool IsChoose { get; set; }

    }
}
