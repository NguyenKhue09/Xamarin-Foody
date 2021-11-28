using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Foody.Models.Local
{
    public class aisle
    {

        public string aisleName { get; set; }

        public ObservableCollection<CartIngredient> Ingredients { get; set; }
    }
}
