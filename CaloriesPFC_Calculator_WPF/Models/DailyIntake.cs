using System;

namespace CaloriesPFC_Calculator_WPF.Models
{
    class DailyIntake
    {
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }

        public DailyIntake()
        {
        }
        public DailyIntake(float calories, float proteins, float fats, float carbohydrates)
        {
            Calories = calories;
            Proteins = proteins;
            Fats = fats;
            Carbohydrates = carbohydrates;
        }
    }
}
