using CaloriesPFC_Calculator_WPF.Infrastracture.Enums;
using System;

namespace CaloriesPFC_Calculator_WPF.Models
{
    class Calculator
    {
        public int Age { get; set; }
        public float HeightCm { get; set; }
        public float WeightKg { get; set; }
        public Sex Sex { get; set; }
        public PhysicalActivity PhysicalActivity { get; set; }
        
        public Calculator() { }
        public Calculator(float height, float weight, int age, Sex sex, PhysicalActivity physicalActivity)
        {
            HeightCm = height;
            WeightKg = weight;
            Age = age;
            Sex = sex;
            PhysicalActivity = physicalActivity;

        }

        public DailyIntake GetDailyIntake()
        {
            DailyIntake dailyIntake = new DailyIntake();
            dailyIntake.DailyCaloriesIntake = (10 * WeightKg) +
                    (6.25f * HeightCm) - (5 * Age);
            if (Sex == Sex.Male)
                dailyIntake.DailyCaloriesIntake += 5f;
            else if (Sex == Sex.Female)
                dailyIntake.DailyCaloriesIntake -= 161f;

            ChangeIntakePhysicalActivity(dailyIntake, PhysicalActivity);

            dailyIntake.DailyProteinsIntake = dailyIntake.DailyCaloriesIntake * 0.3f;
            dailyIntake.DailyFatsIntake = dailyIntake.DailyCaloriesIntake * 0.3f;
            dailyIntake.DailyCarbohydratesIntake = dailyIntake.DailyCaloriesIntake * 0.4f;

            return dailyIntake;
        }

        private DailyIntake ChangeIntakePhysicalActivity(DailyIntake dailyIntake, 
            PhysicalActivity physicalActivity)
        {
            switch (physicalActivity)
            {
                case PhysicalActivity.Absent:
                    dailyIntake.DailyCaloriesIntake *= 1.2f;
                    break;
                case PhysicalActivity.Low:
                    dailyIntake.DailyCaloriesIntake *= 1.375f;
                    break;
                case PhysicalActivity.Medium:
                    dailyIntake.DailyCaloriesIntake *= 1.55f;
                    break;
                case PhysicalActivity.High:
                    dailyIntake.DailyCaloriesIntake *= 1.725f;
                    break;
                case PhysicalActivity.SuperHigh:
                    dailyIntake.DailyCaloriesIntake *= 1.9f;
                    break;
            }

            return dailyIntake;
        }
    }
}
