using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesPFC_Calculator_WPF.Models
{
    internal class DayRation
    {
        public DateTime Date { get; set; }
        public IEnumerable<Meal>? Meals { get; set; }
        public float Calories
        {
            get
            {
                float tempCalories = 0f;
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        tempCalories += meal.Calories;
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
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        tempProteins += meal.Proteins;
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
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        tempFats += meal.Fats;
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
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        tempCarbohydrates += meal.Carbohydrates;
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
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        tempWeight += meal.WeightGrams;
                    }
                }

                return tempWeight;
            }
        }

    }
}
