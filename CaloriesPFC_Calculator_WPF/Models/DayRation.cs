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
        private float _calories = 0;
        public float Calories 
        {
            get
            {
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        _calories += meal.Calories;
                    }
                }

                return _calories;
            }
            set
            {
                if (value < 0) throw new ArgumentException(nameof(value));
                _calories = value;
            }
        }
        private float _proteins;
        public float Proteins
        {
            get
            {
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        _proteins += meal.Proteins;
                    }
                }

                return _proteins;
            }
            set
            {
                if (value < 0) throw new ArgumentException(nameof(value));

                _proteins = value;
            }
        }
        private float _fats;
        public float Fats
        {
            get
            {
                if (Meals != null)
                {
                    foreach (Meal meal in Meals)
                    {
                        _fats += meal.Fats;
                    }
                }

                return _fats;
            }
            set
            {
                if (value < 0) throw new ArgumentException(nameof(value));

                _fats = value;
            }
        }
        private float _carbohydrates;
        public float Carbohydrates
        {
            get
            {
                if (Meals != null)
                {
                    foreach(Meal meal in Meals)
                    {
                        _carbohydrates += meal.Carbohydrates;
                    }
                }

                return _carbohydrates;
            }
            set
            {
                if (value < 0) throw new ArgumentException(nameof(value));

                _carbohydrates = value;
            }
        }

    }
}
