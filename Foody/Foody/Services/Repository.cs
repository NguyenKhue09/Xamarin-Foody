﻿
using Foody.Models;
using System.Collections.Generic;
using System.Linq;

namespace Foody.Services
{
    public static class Repository
    {

        static Repository()
        {
            #region Category List
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    Name_Category="Fast food",
                    
                },
                new Category
                {
                    Name_Category="Dessert",
                    
                },
                new Category
                {
                    Name_Category="Beer",
                    
                },
                new Category
                {
                    Name_Category="Gourmet Food",
                   
                },
            };
            #endregion

            #region Food List
            List<Food> foods = new List<Food>
            {
               new Food
                {
                    Id=1,
                    VarietyFoods=new VarietyFood
                    {
                          Name_VarietyFood="Gourmet Food",
                          
                    },
                    Name_Food="Black ramen",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_black_ramen.png",
                    Price_Food=30.00,
                    Short_Description_Food="Thick and milky broth with an intense flavor.",
                    Long_Description_Food="Our Hakata Black Ramen is a rich and thick stock tonkotsu ramen typical of the Hakata area, in Fukuoka (south of the country), which is made by cooking pork bones for hours to obtain a thick and milky broth with an intense flavor that is It is accompanied by strong flavors such as garlic or ginger, resulting in a powerful umami."
                },
                new Food
                {
                    Id=2,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Gourmet Food",
                        
                    },
                    Name_Food="Avocado of the gods",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_avocado_of_the_gods.png",
                    Price_Food=14.00,
                    Short_Description_Food="Whole wheat toast with avocado cream and boiled egg.",
                    Long_Description_Food="From time to time we also like to have toast for breakfast. And since we know that many of you do not know very well what to accompany them, here is an idea of ​​a complete and healthy breakfast: whole wheat toast with avocado cream and boiled egg, the perfect combination to fill you with energy."
                },
                new Food
                {
                    Id=3,
                    VarietyFoods=new VarietyFood
                    {
                      Name_VarietyFood="Gourmet Food",
                      
                    },
                    Name_Food="Arctic salad",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_arctic_salad.png",
                    Price_Food=26.99,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=4,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Gourmet Food",
                        
                    },
                    Name_Food="Vegetable streaky",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_vegetable_streaky.png",
                    Price_Food=19.55,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=5,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Gourmet Food",
                        
                    },
                    Name_Food="Pumpkin cream",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_pumpkin_cream.png",
                    Price_Food=27.55,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=6,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Gourmet Food",
                        
                    },
                    Name_Food="Paradise salad",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_paradise_salad.png",
                    Price_Food=24.75,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=7,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Gourmet Food",
                        
                    },
                    Name_Food="Fruit salad",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Foods/img_fruit_salad.png",
                    Price_Food=19.15,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=8,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Fast food",
                        
                    },
                    Name_Food="Cheese Shack Burger",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Fast_Foods/img_cheese_shack_burger.png",
                    Price_Food=5.99,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=9,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Fast food",
                        
                    },
                    Name_Food="Patty Bun",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Fast_Foods/img_patty_bun.png",
                    Price_Food=8.99,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=10,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Fast food",
                       
                    },
                    Name_Food="Vegetarian Hamburger",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Fast_Foods/img_vegetarian_hamburger.png",
                    Price_Food=12.59,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=11,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Dessert",
                        
                    },
                    Name_Food="Chocolate Gradient",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Ice_Creams/img_chocolate_gradient.png",
                    Price_Food=12.49,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=12,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Dessert",
                        
                    },
                    Name_Food="Creamy Fusion",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Ice_Creams/img_creamy_fusion.png",
                    Price_Food=16.89,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=13,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Dessert",
                        
                    },
                    Name_Food="Magic Cone",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Ice_Creams/img_magic_cone.png",
                    Price_Food=27.49,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=14,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Dessert",
                        
                    },
                    Name_Food="Sweet Oasis",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Ice_Creams/img_sweet_oasis.png",
                    Price_Food=32.59,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=15,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Beer",
                        
                    },
                    Name_Food="Bintang",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Beers/img_bintang.png",
                    Price_Food=5.59,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=16,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Beer",
                       
                    },
                    Name_Food="Carlsberg",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Beers/img_carlsberg.png",
                    Price_Food=4.99,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
                new Food
                {
                    Id=17,
                    VarietyFoods=new VarietyFood
                    {
                        Name_VarietyFood="Beer",
                        
                    },
                    Name_Food="Corona",
                    Image_Food="https://raw.githubusercontent.com/danielmonettelli/Area51/main/Beers/img_corona.png",
                    Price_Food=9.99,
                    Short_Description_Food="Lorem ipsum dolor sit amet",
                    Long_Description_Food="Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum."
                },
            };
            #endregion

            Categories = new List<Category>(categories);
            Foods = new List<Food>(foods);
        }

        public static List<Food> Foods;

        public static List<Category> Categories;
    }

}