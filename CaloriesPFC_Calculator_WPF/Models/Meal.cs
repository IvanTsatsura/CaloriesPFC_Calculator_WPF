using CaloriesPFC_Calculator_WPF.Infrastracture.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesPFC_Calculator_WPF.Models
{
    internal class Meal
    {
        public IEnumerable<Dish>? Products { get; set; }
        public MealType Type { get; set; }
        public float Calories
        {
            get
            {
                float tempCalories = 0f;
                if (Products != null)
                {
                    foreach (Dish product in Products)
                    {
                        tempCalories += product.Calories;
                    }
                }

                return tempCalories;
            }
        }
        public float Proteins
        {
            get
            {
                float tempProteins = 0f;
                if (Products != null)
                {
                    foreach (Dish product in Products)
                    {
                        tempProteins += product.Proteins;
                    }
                }

                return tempProteins;
            }
        }
        public float Fats
        {
            get
            {
                float tempFats = 0f;
                if (Products != null)
                {
                    foreach (Dish product in Products)
                    {
                        tempFats += product.Fats;
                    }
                }

                return tempFats;
            }
        }
        public float Carbohydrates
        {
            get
            {
                float tempCarbohydrates = 0f;
                if (Products != null)
                {
                    foreach (Dish product in Products)
                    {
                        tempCarbohydrates += product.Carbohydrates;
                    }
                }

                return tempCarbohydrates;
            }
        }
        public float WeightGrams
        {
            get
            {
                float tempWeight = 0f;
                if (Products != null)
                {
                    foreach (Dish product in Products)
                    {
                        tempWeight += product.WeightGrams;
                    }
                }

                return tempWeight;
            }
        }
    }
}
