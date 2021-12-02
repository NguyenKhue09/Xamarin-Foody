using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Foody.Constants
{
    public static class Constants
    {
       
        public static string BASEURL = "https://api.spoonacular.com";
        public static string BASE_IMAGE_URL = "https://spoonacular.com/cdn/ingredients_100x100/";
        public static int  NUMBER = 50;
        //public static string APIKEY = "654a09cc5b3549ab9d6442ccd23f9ef3";
        //public static string APIKEY = "fb0553d9de4949eb9494d7a43de9f60c";
       // public static string APIKEY = "e8096985f30b45bf8e3212115a218cc5";
          //public static string APIKEY = "8742b6ff28af45379a5f06320dba113b";
          public static string APIKEY = "409407cdfa02474f8e61e7f643a7dbec";
        //public static string APIKEY = "85e55bb723d44cffbbfcccd99a68225d";
        //public static string APIKEY = "ef20217a3df647f8ae51ed4271f68858";

        public static string RECIPE_TYPE = "vegetarian";
        public static string DIET = "vegan";
        public static bool  ADDRECIPEINFORMATION = true;
        public static bool  FILLINGREDIENTS = true;
        public static bool  RECIPENUTRITION = true;

        public static string RANDOM_RECIPE_TYPE = "beverage,breakfast,side dish";

        public static string POPULAR_RECIPE_TYPE = "marinade,fingerfood,snack,drink";
        public static string POPULAR_DIET = "Whole30,Paleo,Primal,Vegetarian,Vegan,Pescetarian";

        // shopping list
        public static string USER_NAME = "vi-tv-n";
        public static string HASH_USERNAME = "22535350fb09c8cf0206880c0207018f670cabe0";
        // search shopping list 
        public static int NUMBER_SEARCH = 3;
        public static string SORT = "calories";
        public static string QUERY;
        public static string SORT_DIRECTION = "desc";




        public const string DatabaseFilename = "PantrySQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
