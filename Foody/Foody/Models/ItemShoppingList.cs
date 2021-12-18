using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    public class ItemShoppingList
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
    }
}
