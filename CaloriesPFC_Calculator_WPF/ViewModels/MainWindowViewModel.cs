﻿using CaloriesPFC_Calculator_WPF.Infrastracture.Enums;
using CaloriesPFC_Calculator_WPF.Models;
using CaloriesPFC_Calculator_WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesPFC_Calculator_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Title property : string
        private string _title = "CPFC_Calculator";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region All DayRations property : IEnumerable<DayRation>
        public ObservableCollection<DayRation>? DayRations { get; }
        #endregion

        #region All Products property : IEnumerable<Product>
        public ObservableCollection<Product>? Products { get; }
        #endregion

        #region Selected DayRation field + property : DayRation
        private DayRation? _selectedDayRation;
        public DayRation SelectedDayRation
        {
            get => _selectedDayRation;
            set => Set(ref _selectedDayRation, value);
        }
        #endregion

        #region Selected Product field + property : Product
        private Product? _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }
        #endregion

        #region Dates property : DateTime
        public ObservableCollection<DateTime>? Dates { get; }
        #endregion

        public MainWindowViewModel()
        {
            Product tomato = new Product()
            {
                Name = "Tomato",
                Calories = 15.5f,
                Proteins = 1.3f,
                Fats = 0.8f,
                Carbohydrates = 4.0f
            };
            Product egg = new Product()
            {
                Name = "Egg",
                Calories = 25.5f,
                Proteins = 7.5f,
                Fats = 2.6f,
                Carbohydrates = 4.0f
            };
            Product chicken = new Product()
            {
                Name = "Chicken",
                Calories = 130.5f,
                Proteins = 28.4f,
                Fats = 3.2f,
                Carbohydrates = 23.9f
            };
            Product pasta = new Product()
            {
                Name = "Pasta",
                Calories = 334.5f,
                Proteins = 13f,
                Fats = 4.6f,
                Carbohydrates = 71.0f
            };

            Meal meal1 = new Meal();
            meal1.Products = new Product[] { tomato, egg };
            meal1.Type = MealType.Breakfast;

            Meal meal2 = new Meal();
            meal2.Products = new Product[] { chicken, tomato };
            meal2.Type = MealType.Lunch;

            Meal meal3 = new Meal();
            meal3.Products = new Product[] { chicken, tomato, pasta };
            meal3.Type = MealType.Dinner;

            DayRation day1 = new DayRation();
            day1.Meals = new Meal[] { meal1 };
            day1.Date = DateTime.Today.Date;

            DayRation day2 = new DayRation();
            day2.Meals = new Meal[] { meal1, meal2 };
            day2.Date = DateTime.Today.Date.AddDays(1);

            DayRation day3 = new DayRation();
            day3.Meals = new Meal[] { meal1, meal2, meal3 };
            day3.Date = DateTime.Today.Date.AddDays(5);

            Product[] Prds = new Product[] { tomato, egg, chicken, pasta };
            DayRation[] DRArr = new DayRation[] { day1, day2, day3 };
            DayRations = new ObservableCollection<DayRation>(DRArr.OrderBy(x => x.Date));
            Products = new ObservableCollection<Product>(Prds.OrderBy(x => x.Name));  
        }
    }
}
