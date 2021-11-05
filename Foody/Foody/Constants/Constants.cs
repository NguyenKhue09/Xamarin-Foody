﻿using System;
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
        public static string APIKEY = "8b0d84ce7ec14719b84aa7deb47154bf";
        public static string RECIPE_TYPE = "vegetarian";
        public static string DIET = "vegan";
        public static bool  ADDRECIPEINFORMATION = true;
        public static bool  FILLINGREDIENTS = true;
        public static bool  RECIPENUTRITION = true;

        public static string POPULAR_RECIPE_TYPE = "marinade,fingerfood,snack,drink";
        public static string POPULAR_DIET = "Gluten Free,Vegetarian,vegan";

        // shopping list
        public static string USER_NAME = "vi-tv-n";
        public static string HASH_USERNAME = "22535350fb09c8cf0206880c0207018f670cabe0";


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
