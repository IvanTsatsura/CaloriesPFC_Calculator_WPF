using System;

namespace CaloriesPFC_Calculator_WPF.Models
{
    class DailyIntake
    {
        public float DailyCaloriesIntake { get; set; }
        public float DailyProteinsIntake { get; set; }
        public float DailyFatsIntake { get; set; }
        public float DailyCarbohydratesIntake { get; set; }

        public DailyIntake()
        {
        }
        public DailyIntake(float calories, float proteins, float fats, float carbohydrates)
        {
            DailyCaloriesIntake = calories;
            DailyProteinsIntake = proteins;
            DailyFatsIntake = fats;
            DailyCarbohydratesIntake = carbohydrates;
        }
    }
}
