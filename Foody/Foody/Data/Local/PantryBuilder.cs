//using Foody.Models.Local;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Foody.Data.Local
//{
//    public class PantryBuilder
//    {
//        public List<PantryBuilderItem> pantryBuilderItems = new List<PantryBuilderItem> {
//            new PantryBuilderItem
//            {
//                id = 11090,
//                name = "broccoli",
//                aisle = "Produce",
//                image = "broccoli.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 11135,
//                name = "cauliflower",
//                aisle = "Produce",
//                image = "cauliflower.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 4047,
//                name = "coconut oil",
//                aisle = "Health Foods;Baking",
//                image = "oil-coconut.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 20041,
//                name = "cooked brown rice",
//                aisle = "Pasta and Rice",
//                image = "rice-brown-cooked.png",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 11215,
//                name = "garlic",
//                aisle = "Produce",
//                image = "garlic.png",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 4517,
//                name = "grapeseed oil",
//                aisle = "Oil, Vinegar, Salad Dressing",
//                image = "vegetable-oil.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 16424,
//                name = "low sodium soy sauce",
//                aisle = "Ethnic Foods;Condiments",
//                image = "soy-sauce.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 11304,
//                name = "peas",
//                aisle = "Produce",
//                image = "peas.jpg" ,
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 2047,
//                name = "salt",
//                aisle = "Spices and Seasonings",
//                image = "salt.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 11291,
//                name = "scallion",
//                aisle = "Produce",
//                image = "spring-onions.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 11291,
//                name = "scallion",
//                aisle = "Produce",
//                image = "spring-onions.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 4058,
//                name = "sesame oil",
//                aisle = "Ethnic Foods",
//                image = "sesame-oil.png",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 12023,
//                name = "sesame seeds",
//                aisle = "Ethnic Foods;Spices and Seasonings",
//                image = "sesame-seeds.png",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 2044,
//                name = "basil",
//                aisle = "Produce",
//                image = "fresh-basil.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 20081,
//                name = "flour",
//                aisle = "Baking",
//                image = "flour.png",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 1022020,
//                name = "garlic powder",
//                aisle = "Spices and Seasonings",
//                image = "garlic-powder.png",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 1062047,
//                name = "garlic salt",
//                aisle = "Spices and Seasonings",
//                image = "garlic-salt.jpg",
//                isChoose = false
//            },
//            new PantryBuilderItem
//            {
//                id = 11352,
//                name = "potatoes",
//                aisle = "Produce",
//                image = "potatoes-yukon-gold.png",
//                isChoose = false
//            },
//             new PantryBuilderItem
//             {
//                id = 2069,
//                name = "balsamic vinegar",
//                aisle = "Oil, Vinegar, Salad Dressing",
//                image = "balsamic-vinegar.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11215,
//                name = "garlic",
//                aisle = "Produce",
//                image = "garlic.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11233,
//                name = "kale",
//                aisle = "Produce",
//                image = "kale.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 4053,
//                name = "olive oil",
//                aisle = "Oil, Vinegar, Salad Dressing",
//                image = "olive-oil.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 20040,
//                name = "brown rice",
//                aisle = "Pasta and Rice",
//                image = "uncooked-brown-rice.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11124,
//                name = "carrots",
//                aisle = "Produce",
//                image = "sliced-carrot.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11143,
//                name = "celery",
//                aisle = "Produce",
//                image = "celery.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 2007,
//                name = "celery seed",
//                aisle = "Spices and Seasonings",
//                image = "celery-seed.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 16032,
//                name = "dried kidney beans",
//                aisle = "Pasta and Rice;Canned and Jarred",
//                image = "kidney-beans.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 2023,
//                name = "dried marjoram",
//                aisle = "Produce;Spices and Seasonings",
//                image = "marjoram.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 2042,
//                name = "dried thyme",
//                aisle = "Spices and Seasonings",
//                image = "thyme.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11209,
//                name = "eggplant",
//                aisle = "Produce",
//                image = "eggplant.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11052,
//                name = "green beans",
//                aisle = "Produce",
//                image = "green-beans-or-string-beans.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1002030,
//                name = "ground pepper",
//                aisle = "Spices and Seasonings",
//                image = "pepper.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1012038,
//                name = "ground sage",
//                aisle = "Spices and Seasonings",
//                image = "dried-sage.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 93627,
//                name = "liquid smoke",
//                aisle = "Spices and Seasonings;Condiments",
//                image = "dark-sauce.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 4053,
//                name = "olive oil",
//                aisle = "Oil, Vinegar, Salad Dressing",
//                image = "olive-oil.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11821,
//                name = "red bell pepper",
//                aisle = "Produce",
//                image = "red-pepper.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 10011282,
//                name = "red onion",
//                aisle = "Produce",
//                image = "red-onion.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1012047,
//                name = "sea salt",
//                aisle = "Spices and Seasonings",
//                image = "salt.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1016168,
//                name = "sriracha",
//                aisle = "Condiments",
//                image = "hot-sauce-or-tabasco.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11529,
//                name = "tomatoes",
//                aisle = "Produce",
//                image = "tomato.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 6615,
//                name = "vegetable stock",
//                aisle = "Canned and Jarred",
//                image = "chicken-broth.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 16018,
//                name = "canned black beans",
//                aisle = "Canned and Jarred",
//                image = "black-beans.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 10011693,
//                name = "canned tomatoes",
//                aisle = "Canned and Jarred",
//                image = "tomatoes-canned.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 2009,
//                name = "chili powder",
//                aisle = "Spices and Seasonings",
//                image = "chili-powder.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1002014,
//                name = "cumin",
//                aisle = "Spices and Seasonings",
//                image = "ground-cumin.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 6168,
//                name = "hot sauce",
//                aisle = "Condiments",
//                image = "hot-sauce-or-tabasco.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11282,
//                name = "onion",
//                aisle = "Produce",
//                image = "brown-onion.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 20444,
//                name = "rice",
//                aisle = "Pasta and Rice",
//                image = "uncooked-white-rice.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 14412,
//                name = "water",
//                aisle = "Beverages",
//                image = "water.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 16063,
//                name = "black eyed peas",
//                aisle = "Pasta and Rice;Canned and Jarred",
//                image = "black-eyed-peas.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 2015,
//                name = "curry powder",
//                aisle = "Spices and Seasonings",
//                image = "curry-powder.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 93663,
//                name = "garam masala",
//                aisle = "Ethnic Foods;Spices and Seasonings",
//                image = "garam-masala.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11209,
//                name = "globe eggplant",
//                aisle = "Produce",
//                image = "eggplant.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1002013,
//                name = "ground coriander",
//                aisle = "Spices and Seasonings",
//                image = "ground-coriander.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1002014,
//                name = "ground cumin",
//                aisle = "Spices and Seasonings",
//                image = "ground-cumin.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1002024,
//                name = "mustard powder",
//                aisle = "Spices and Seasonings",
//                image = "dry-mustard.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 10018617,
//                name = "graham cracker crumbs",
//                aisle = "Sweet Snacks;Baking",
//                image = "graham-crackers.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 16223,
//                name = "soy milk",
//                aisle = "Milk, Eggs, Other Dairy",
//                image = "soy-milk.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1119,
//                name = "vanilla yogurt",
//                aisle = "Milk, Eggs, Other Dairy",
//                image = "vanilla-yogurt.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1002046,
//                name = "dijon mustard",
//                aisle = "Condiments",
//                image = "dijon-mustard.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 19296,
//                name = "honey",
//                aisle = "Nut butters, Jams, and Honey",
//                image = "honey.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1022068,
//                name = "red wine vinegar",
//                aisle = "Oil, Vinegar, Salad Dressing",
//                image = "red-wine-vinegar.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 12155,
//                name = "walnuts",
//                aisle = "Nuts;Baking",
//                image = "walnuts.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11913,
//                name = "frozen corn",
//                aisle = "Frozen",
//                image = "corn.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 11913,
//                name = "frozen corn",
//                aisle = "Frozen",
//                image = "corn.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 20028,
//                name = "couscous",
//                aisle = "Pasta and Rice;Ethnic Foods;Health Foods",
//                image = "couscous-cooked.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 12151,
//                name = "pistachio nuts",
//                aisle = "Nuts;Savory Snacks",
//                image = "pistachios.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 10111352,
//                name = "fingerling potatoes",
//                aisle = "Frozen",
//                image = "fingerling-potatoes.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 20081,
//                name = "flour",
//                aisle = "Baking",
//                image = "flour.png",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 1090,
//                name = "powdered milk",
//                aisle = "Baking",
//                image = "milk-powdered.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 18375,
//                name = "yeast",
//                aisle = "Baking",
//                image = "yeast-granules.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 20027,
//                name = "cornflour",
//                aisle = "Baking",
//                image = "white-powder.jpg",
//                isChoose = false
//             },
//             new PantryBuilderItem
//             {
//                id = 20012,
//                name = "bulgur wheat",
//                aisle = "Pasta and Rice;Ethnic Foods;Health Foods",
//                image = "bulgur-wheat.jpg",
//                isChoose = false
//             },
//        };
//    }
//}
