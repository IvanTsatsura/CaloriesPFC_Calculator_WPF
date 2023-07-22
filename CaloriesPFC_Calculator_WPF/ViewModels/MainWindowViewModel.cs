using CaloriesPFC_Calculator_WPF.Infrastracture.Commands;
using CaloriesPFC_Calculator_WPF.Infrastracture.Enums;
using CaloriesPFC_Calculator_WPF.Models;
using CaloriesPFC_Calculator_WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

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

        #region All Dishes property : IEnumerable<Product>
        public IList<Dish>? Dishes { get; }
        #endregion

        #region Filtred Dishes property : IEnumerable<Product>
        private IList<Dish> _filtredDishes;
        public IList<Dish>? FiltredDishes 
        {
            get
            {
                if (_filtredDishes == null)
                    _filtredDishes = Dishes;
                return _filtredDishes;
            }
            set => Set(ref _filtredDishes, value); 
        }
        #endregion

        #region Selected DayRation field + property : DayRation
        private DayRation? _selectedDayRation;
        public DayRation SelectedDayRation
        {
            get => _selectedDayRation;
            set => Set(ref _selectedDayRation, value);
        }
        #endregion

        #region Selected Dish field + property : Product
        private Dish? _selectedDish;
        public Dish SelectedDish
        {
            get => _selectedDish;
            set => Set(ref _selectedDish, value);
        }
        #endregion
        #region ProductFilterText : string
        private string _productFilterText;

        public string ProductFilterText
        {
            get => _productFilterText;
            set
            {
                if(!Set(ref _productFilterText, value)) return;
                OnPropertyChanged(nameof(FiltredDishes));
                
            }
        }
        #endregion


        #region Dates property : DateTime
        public ObservableCollection<DateTime>? Dates { get; }
        #endregion

        #region Commands
        #region Delete dish command
        public ICommand DeleteDishCommand { get; }
        private bool CanDeleteDishCommandExecute(object p) => p is Dish dish && 
            Dishes != null && Dishes.Contains(dish);
        private void OnDeleteDishCommandExecuted(object p)
        {
            if (!(p is Dish dish)) return;
            Dishes.Remove(dish);
        }
        #endregion

        #region SearchCommand
        public ICommand SearchDishCommand { get; }
        private bool CanSearchProductCommandExecute(object p) => true;
        private void OnSearchProductCommandExecuted(object p)
        {
            if (!(p is string name)) return;
            if (Dishes != null)
                FiltredDishes = Dishes
                    .Where(d => d.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            OnPropertyChanged(nameof(FiltredDishes));
        }
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            #region Commands

            DeleteDishCommand = new RelayCommand(OnDeleteDishCommandExecuted,
                CanDeleteDishCommandExecute);
            SearchDishCommand = new RelayCommand(OnSearchProductCommandExecuted,
                CanSearchProductCommandExecute);

            #endregion

            Dish tomato = new Dish()
            {
                Name = "Tomato",
                Calories = 15.5f,
                Proteins = 1.3f,
                Fats = 0.8f,
                Carbohydrates = 4.0f
            };
            Dish egg = new Dish()
            {
                Name = "Egg",
                Calories = 25.5f,
                Proteins = 7.5f,
                Fats = 2.6f,
                Carbohydrates = 4.0f
            };
            Dish chicken = new Dish()
            {
                Name = "Chicken",
                Calories = 130.5f,
                Proteins = 28.4f,
                Fats = 3.2f,
                Carbohydrates = 23.9f
            };
            Dish pasta = new Dish()
            {
                Name = "Pasta",
                Calories = 334.5f,
                Proteins = 13f,
                Fats = 4.6f,
                Carbohydrates = 71.0f
            };

            Meal meal1 = new Meal();
            meal1.Products = new Dish[] { tomato, egg };
            meal1.Type = MealType.Breakfast;

            Meal meal2 = new Meal();
            meal2.Products = new Dish[] { chicken, tomato };
            meal2.Type = MealType.Lunch;

            Meal meal3 = new Meal();
            meal3.Products = new Dish[] { chicken, tomato, pasta };
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

            Dish[] Prds = new Dish[] { tomato, egg, chicken, pasta };
            DayRation[] DRArr = new DayRation[] { day1, day2, day3 };
            DayRations = new ObservableCollection<DayRation>(DRArr.OrderBy(x => x.Date));
            Dishes = new List<Dish>(Prds.OrderBy(x => x.Name));  
        }
    }
}
