using Foody.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Foody.ViewModels
{
    public class DetailRecipeViewModel: BaseViewModel
    {
        

        public Result recipe { get; set; }

       
        public Rectangle rect { get; set; }

        

        public DetailRecipeViewModel(Result result)
        {
            recipe = result;
            rect = new Rectangle(0, 0, 0.5, 1);
        }

        public Rectangle setProgresBarValue(Double value)
        {
            return new Rectangle(0, 0, value, 1);
        }

    }
}
