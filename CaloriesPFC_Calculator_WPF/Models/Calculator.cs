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
        public Calculator(float heightCm, float weightKg, int age, Sex sex, PhysicalActivity physicalActivity)
        {
            HeightCm = heightCm;
            WeightKg = weightKg;
            Age = age;
            Sex = sex;
            PhysicalActivity = physicalActivity;

        }

        public DailyIntake GetDailyIntake()
        {
            DailyIntake dailyIntake = new DailyIntake();
            dailyIntake.Calories = (10 * WeightKg) +
                    (6.25f * HeightCm) - (5 * Age);
            if (Sex == Sex.Male)
                dailyIntake.Calories += 5f;
            else if (Sex == Sex.Female)
                dailyIntake.Calories -= 161f;

            ChangeIntakePhysicalActivity(dailyIntake, PhysicalActivity);

            dailyIntake.Proteins = (float)Math.Round(dailyIntake.Calories * 0.3f / 4);
            dailyIntake.Fats = (float)Math.Round(dailyIntake.Calories * 0.3f / 9);
            dailyIntake.Carbohydrates = (float)Math.Round(dailyIntake.Calories * 0.4f / 4);
            dailyIntake.Calories = (float)Math.Round(dailyIntake.Calories);

            return dailyIntake;
        }

        private DailyIntake ChangeIntakePhysicalActivity(DailyIntake dailyIntake, 
            PhysicalActivity physicalActivity)
        {
            switch (physicalActivity)
            {
                case PhysicalActivity.Absent:
                    dailyIntake.Calories *= 1.2f;
                    break;
                case PhysicalActivity.Low:
                    dailyIntake.Calories *= 1.375f;
                    break;
                case PhysicalActivity.Medium:
                    dailyIntake.Calories *= 1.55f;
                    break;
                case PhysicalActivity.High:
                    dailyIntake.Calories *= 1.725f;
                    break;
                case PhysicalActivity.SuperHigh:
                    dailyIntake.Calories *= 1.9f;
                    break;
            }

            return dailyIntake;
        }
    }
}
