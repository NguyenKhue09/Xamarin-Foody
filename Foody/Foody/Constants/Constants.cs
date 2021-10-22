using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Constants
{
    public static class Constants
    {
       
        public static string BASEURL = "https://api.spoonacular.com";
        public static string BASE_IMAGE_URL = "https://spoonacular.com/cdn/ingredients_100x100/";
        public static int  NUMBER = 50;
        public static string APIKEY = "654a09cc5b3549ab9d6442ccd23f9ef3";
        public static string RECIPE_TYPE = "vegetarian";
        public static string DIET = "vegan";
        public static bool  ADDRECIPEINFORMATION = true;
        public static bool  FILLINGREDIENTS = true;
        public static bool  RECIPENUTRITION = true;

        public static string POPULAR_RECIPE_TYPE = "marinade,fingerfood,snack,drink";
        public static string POPULAR_DIET = "Gluten Free,Vegetarian,vegan";
    }
}
