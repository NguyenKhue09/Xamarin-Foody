using Foody.Models.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Data.Local
{
    class PantryBuilder
    {
        List<PantryBuilderItem> pantryBuilderItems = new List<PantryBuilderItem> {
            new PantryBuilderItem
            {
                ingredientId = 11090,
                ingredientName = "broccoli",
                aisleBelong = "Produce",
                ingredientImg = "broccoli.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 11135,
                ingredientName = "cauliflower",
                aisleBelong = "Produce",
                ingredientImg = "cauliflower.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 4047,
                ingredientName = "coconut oil",
                aisleBelong = "Health Foods;Baking",
                ingredientImg = "oil-coconut.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 20041,
                ingredientName = "cooked brown rice",
                aisleBelong = "Pasta and Rice",
                ingredientImg = "rice-brown-cooked.png"
            },
            new PantryBuilderItem
            {
                ingredientId = 11215,
                ingredientName = "garlic",
                aisleBelong = "Produce",
                ingredientImg = "garlic.png"
            },
            new PantryBuilderItem
            {
                ingredientId = 4517,
                ingredientName = "grapeseed oil",
                aisleBelong = "Oil, Vinegar, Salad Dressing",
                ingredientImg = "vegetable-oil.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 16424,
                ingredientName = "low sodium soy sauce",
                aisleBelong = "Ethnic Foods;Condiments",
                ingredientImg = "soy-sauce.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 11304,
                ingredientName = "peas",
                aisleBelong = "Produce",
                ingredientImg = "peas.jpg" 
            },
            new PantryBuilderItem
            {
                ingredientId = 2047,
                ingredientName = "salt",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "salt.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 11291,
                ingredientName = "scallion",
                aisleBelong = "Produce",
                ingredientImg = "spring-onions.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 11291,
                ingredientName = "scallion",
                aisleBelong = "Produce",
                ingredientImg = "spring-onions.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 4058,
                ingredientName = "sesame oil",
                aisleBelong = "Ethnic Foods",
                ingredientImg = "sesame-oil.png"
            },
            new PantryBuilderItem
            {
                ingredientId = 12023,
                ingredientName = "sesame seeds",
                aisleBelong = "Ethnic Foods;Spices and Seasonings",
                ingredientImg = "sesame-seeds.png"
            },
            new PantryBuilderItem
            {
                ingredientId = 2044,
                ingredientName = "basil",
                aisleBelong = "Produce",
                ingredientImg = "fresh-basil.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 20081,
                ingredientName = "flour",
                aisleBelong = "Baking",
                ingredientImg = "flour.png"
            },
            new PantryBuilderItem
            {
                ingredientId = 1022020,
                ingredientName = "garlic powder",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "garlic-powder.png"
            },
            new PantryBuilderItem
            {
                ingredientId = 1062047,
                ingredientName = "garlic salt",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "garlic-salt.jpg"
            },
            new PantryBuilderItem
            {
                ingredientId = 11352,
                ingredientName = "potatoes",
                aisleBelong = "Produce",
                ingredientImg = "potatoes-yukon-gold.png"
            },
             new PantryBuilderItem
             {
                ingredientId = 2069,
                ingredientName = "balsamic vinegar",
                aisleBelong = "Oil, Vinegar, Salad Dressing",
                ingredientImg = "balsamic-vinegar.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 11215,
                ingredientName = "garlic",
                aisleBelong = "Produce",
                ingredientImg = "garlic.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 11233,
                ingredientName = "kale",
                aisleBelong = "Produce",
                ingredientImg = "kale.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 4053,
                ingredientName = "olive oil",
                aisleBelong = "Oil, Vinegar, Salad Dressing",
                ingredientImg = "olive-oil.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 20040,
                ingredientName = "brown rice",
                aisleBelong = "Pasta and Rice",
                ingredientImg = "uncooked-brown-rice.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 11124,
                ingredientName = "carrots",
                aisleBelong = "Produce",
                ingredientImg = "sliced-carrot.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 11143,
                ingredientName = "celery",
                aisleBelong = "Produce",
                ingredientImg = "celery.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 2007,
                ingredientName = "celery seed",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "celery-seed.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 16032,
                ingredientName = "dried kidney beans",
                aisleBelong = "Pasta and Rice;Canned and Jarred",
                ingredientImg = "kidney-beans.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 2023,
                ingredientName = "dried marjoram",
                aisleBelong = "Produce;Spices and Seasonings",
                ingredientImg = "marjoram.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 2042,
                ingredientName = "dried thyme",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "thyme.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 11209,
                ingredientName = "eggplant",
                aisleBelong = "Produce",
                ingredientImg = "eggplant.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 11052,
                ingredientName = "green beans",
                aisleBelong = "Produce",
                ingredientImg = "green-beans-or-string-beans.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 1002030,
                ingredientName = "ground pepper",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "pepper.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 1012038,
                ingredientName = "ground sage",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "dried-sage.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 93627,
                ingredientName = "liquid smoke",
                aisleBelong = "Spices and Seasonings;Condiments",
                ingredientImg = "dark-sauce.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 4053,
                ingredientName = "olive oil",
                aisleBelong = "Oil, Vinegar, Salad Dressing",
                ingredientImg = "olive-oil.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 11821,
                ingredientName = "red bell pepper",
                aisleBelong = "Produce",
                ingredientImg = "red-pepper.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 10011282,
                ingredientName = "red onion",
                aisleBelong = "Produce",
                ingredientImg = "red-onion.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 1012047,
                ingredientName = "sea salt",
                aisleBelong = "Spices and Seasonings",
                ingredientImg = "salt.jpg"
             },
             new PantryBuilderItem
             {
                ingredientId = 1016168,
                ingredientName = "sriracha",
                aisleBelong = "Condiments",
                ingredientImg = "hot-sauce-or-tabasco.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 11529,
                ingredientName = "tomatoes",
                aisleBelong = "Produce",
                ingredientImg = "tomato.png"
             },
             new PantryBuilderItem
             {
                ingredientId = 6615,
                ingredientName = "vegetable stock",
                aisleBelong = "Canned and Jarred",
                ingredientImg = "chicken-broth.png"
             },
        };
    }
}
