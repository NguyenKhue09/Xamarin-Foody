using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Models
{
    public class UserPantryItem
    {
        public string userId { get; set; }

        public List<string> itemId { get; set; }
    }
}
