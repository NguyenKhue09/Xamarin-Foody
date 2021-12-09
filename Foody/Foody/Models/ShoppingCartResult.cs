using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    public class ItemCart
    {
        public string _id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string unit { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
        public string userID { get; set; }
    }
    public class ShoppingCartResult
    {
        public List<ItemCart> resultsCart { get; set; }
    }
}
