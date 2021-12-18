using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Item
    {
        public string _id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
    }

    public class ShoppingListResult
    {
        public List<Item> results { get; set; }
    }
}
