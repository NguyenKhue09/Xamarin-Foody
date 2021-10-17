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
        public static string APIKEY = "85e55bb723d44cffbbfcccd99a68225d";
        public static string RECIPE_TYPE = "vegetarian";
        public static string DIET = "vegan";
        public static bool  ADDRECIPEINFORMATION = true;
        public static bool  FILLINGREDIENTS = true;
        public static bool  RECIPENUTRITION = true;

        public static string POPULAR_RECIPE_TYPE = "marinade,fingerfood,snack,drink";
        public static string POPULAR_DIET = "Gluten Free,Vegetarian,vegan";
    }
}
