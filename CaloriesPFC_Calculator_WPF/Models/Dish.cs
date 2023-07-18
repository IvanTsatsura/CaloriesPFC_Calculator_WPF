using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesPFC_Calculator_WPF.Models
{
    internal class Dish
    {
        public string Name { get; set; }
        private float _calories;
        public float Calories
        {
            get
            {
                if (_weightGrams == 100)
                    return _calories;
                else
                    return _calories * _weightGrams / 100;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException(nameof(value));
                _calories = value;
            }
        }
        private float _proteins;
        public float Proteins
        {
            get
            {
                if (_weightGrams == 100)
                    return _proteins;
                else
                    return _proteins * _weightGrams / 100;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException(nameof(value));
                _proteins = value;
            }
        }
        private float _fats;
        public float Fats
        {
            get
            {
                if (_weightGrams == 100)
                    return _fats;
                else
                    return _fats * _weightGrams / 100;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException(nameof(value));
                _fats = value;
            }
        }
        private float _carbohydrates;
        public float Carbohydrates 
        {
            get
            {
                if (_weightGrams == 100)
                    return _carbohydrates;
                else
                    return _carbohydrates * _weightGrams / 100;
            }
            set
            {
                if (value < 0) 
                    throw new ArgumentException(nameof(value));
                _carbohydrates = value;
            }
        }
        private float _weightGrams = 100f;
        public float WeightGrams
        { 
            get => _weightGrams;
            set 
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _weightGrams = value;
            } 
        }
    }
}
