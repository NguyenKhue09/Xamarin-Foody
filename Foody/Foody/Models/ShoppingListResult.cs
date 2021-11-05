using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    public class Original
    {
        public double amount { get; set; }
        public string unit { get; set; }
    }
    public class Item
    {
        public int id { get; set; }
        public string itemImg { get; set; }
        public string name { get; set; }
        public Measures measures { get; set; }
        public List<object> usages { get; set; }
        public bool pantryItem { get; set; }
        public string aisle { get; set; }
        public double cost { get; set; }
        public int ingredientId { get; set; }
    }

    public class Aisle
    {
        public string aisle { get; set; }
        public List<Item> items { get; set; }
    }

    public class ShoppingListResult
    {
        public List<Aisle> aisles { get; set; }
        public double cost { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set; }
    }
}
