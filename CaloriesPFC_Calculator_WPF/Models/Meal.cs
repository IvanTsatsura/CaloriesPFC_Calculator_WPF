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
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public MealType Type { get; set; }
    }
}
