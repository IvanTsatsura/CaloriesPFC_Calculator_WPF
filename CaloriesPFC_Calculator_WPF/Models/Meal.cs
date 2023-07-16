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
        public IEnumerable<Product>? Products { get; set; }
        public MealType Type { get; set; }
        private float _calories = 0;
        public float Calories
        {
            get
            {
                if (Products != null)
                {
                    foreach (Product product in Products)
                    {
                        _calories += product.Calories;
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
                if (Products != null)
                {
                    foreach (Product product in Products)
                    {
                        _proteins += product.Proteins;
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
                if (Products != null)
                {
                    foreach (Product product in Products)
                    {
                        _fats += product.Fats;
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
                if (Products != null)
                {
                    foreach (Product product in Products)
                    {
                        _carbohydrates += product.Carbohydrates;
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
