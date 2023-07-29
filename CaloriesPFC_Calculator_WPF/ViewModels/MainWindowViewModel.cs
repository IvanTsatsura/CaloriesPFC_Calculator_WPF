using CaloriesPFC_Calculator_WPF.Infrastracture.Commands;
using CaloriesPFC_Calculator_WPF.Infrastracture.Enums;
using CaloriesPFC_Calculator_WPF.Models;
using CaloriesPFC_Calculator_WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Serialization;

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

        #region Current DailyIntake property : DailyIntake
        private DailyIntake _currentDailyIntake;
        public DailyIntake CurrentDailyIntake
        {
            get => _currentDailyIntake;
            set
            {
                if (!Set(ref _currentDailyIntake, value)) return;
                OnPropertyChanged(nameof(CurrentDailyIntake));
            }
        }
        #endregion

        #region Today Intake property : DailyIntake
        private DailyIntake _todayIntake;
        public DailyIntake TodayIntake
        {
            get => _todayIntake;
            set
            {
                if (!Set(ref _todayIntake, value)) return;
                OnPropertyChanged(nameof(TodayIntake)); 
            }
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
            set
            {
                if (!Set(ref _filtredDishes, value)) return;
                OnPropertyChanged(nameof(FiltredDishes));
            }
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

        #region DishFilterText : string
        private string _dishFilterText;

        public string DishFilterText
        {
            get => _dishFilterText;
            set
            {
                if(!Set(ref _dishFilterText, value)) return;
                /*OnPropertyChanged(nameof(FiltredDishes));*/
                SearchDishCommand.Execute(value);
            }
        }
        #endregion

        #region For create new dish properties : dish
        public string? NewDishName { get; set; }
        public string? NewDishCalories { get; set; }
        public string? NewDishProteins { get; set; }
        public string? NewDishFats { get; set; }
        public string? NewDishCarbohydrates { get; set; }
        public string? NewDishWeightGrams { get; set; } = "100";
        #endregion

        #region Dates property : DateTime
        public ObservableCollection<DateTime>? Dates { get; }
        #endregion

        #region Calculated DailyIntake field + property : DailyIntake
        private DailyIntake? _dailyIntake;
        public DailyIntake? DailyIntake
        {
            get { return _dailyIntake; }
            set
            {
                if(!Set(ref _dailyIntake, value)) return;
                OnPropertyChanged(nameof(DailyIntake));
            }
        }
        #endregion

        #region Calculator readonly field : Calculator
        public Calculator Calculator { get; }
        #endregion

        #region Calculator properties : string?
        public string? CalculatorAge { get; set; }
        public string? CalculatorHeight { get; set; }
        public string? CalculatorWeight { get; set; }
        #endregion

        #region Selected Dishes For Create New Meal property + field : IList<Dish> 
        private IList<Dish> _selectedDishes;
        public IList<Dish> SelectedDishes
        {
            get => _selectedDishes;
            set
            {
                if(!Set(ref _selectedDishes, value)) return;
                OnPropertyChanged(nameof(SelectedDish));
            }
        }
        #endregion

        #region Meal For Add : Meal
        public Meal Meal { get; set; }
        #endregion

        #region Today Ration : DayRation
        public DayRation TodayRation { get; set; }
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
            FiltredDishes = new List<Dish>();
            FiltredDishes = Dishes;
        }
        #endregion

        #region SearchCommand
        public ICommand SearchDishCommand { get; }
        private bool CanSearchProductCommandExecute(object p) => p is string name &&
            !string.IsNullOrEmpty(name);
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

        #region Create dish command
        public ICommand CreateDishCommand { get; }
        private bool CanCreateDishCommandExecute(object p)
        {
            string pattern = @"^[0-9]*(?:\.[0-9]*)?$";
            Regex regex = new Regex(pattern);
            if (NewDishCalories is null || NewDishCalories=="" ||
                !regex.IsMatch(NewDishCalories))
                return false;
            if (NewDishProteins is null || NewDishProteins == "" ||
                !regex.IsMatch(NewDishProteins))
                return false;
            if (NewDishFats is null || NewDishFats == "" ||
                !regex.IsMatch(NewDishFats))
                return false;
            if (NewDishCarbohydrates is null || NewDishCarbohydrates == "" ||
                !regex.IsMatch(NewDishCarbohydrates))
                return false;
            if (NewDishWeightGrams is null || NewDishWeightGrams == "" ||
                !regex.IsMatch(NewDishWeightGrams))
                return false;

            return true;
        }
        private void OnCreateDishCommandExecuted(object p)
        {
            Dish newDish = new Dish();
            try
            {
                newDish.Name = (string)NewDishName;
                newDish.Calories = float.Parse(NewDishCalories);
                newDish.Proteins = float.Parse(NewDishProteins);
                newDish.Fats = float.Parse(NewDishFats);
                newDish.Carbohydrates = float.Parse(NewDishCarbohydrates);
                newDish.WeightGrams = float.Parse(NewDishWeightGrams);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            if (Dishes != null)
                Dishes.Add(newDish);
            FiltredDishes = new List<Dish>();
            FiltredDishes = Dishes;
            ClearAddFormCommand.Execute(this);
        }
        #endregion

        #region ClearCreateNewDishFormCommand

        public ICommand ClearAddFormCommand { get; }
        private bool CanClearAddFormCommandExecute(object p) => true;
        private void OnClearAddFormCommandExecuted(object p)
        {
            NewDishName = " ";
            NewDishCalories = " ";
            NewDishProteins = " ";
            NewDishFats = " ";
            NewDishCarbohydrates = " ";
            NewDishWeightGrams = "100";
            OnPropertyChanged(nameof(NewDishName));
            OnPropertyChanged(nameof(NewDishCalories));
            OnPropertyChanged(nameof(NewDishProteins));
            OnPropertyChanged(nameof(NewDishFats));
            OnPropertyChanged(nameof(NewDishCarbohydrates));
            OnPropertyChanged(nameof(NewDishWeightGrams));
        }

        #endregion

        #region Calculate DailyIntake Command
        public ICommand CalculateIntakeCommand { get; }
        private bool CanCalculateIntakeCommandExecute(object p)
        {
            string intPattern = @"^\d+$";
            string floatPattern = @"^[0-9]*(?:\.[0-9]*)?$";
            Regex regex = new Regex(intPattern);
            if (CalculatorAge is null || CalculatorAge == "" || !regex.IsMatch(CalculatorAge))
                return false;
            regex = new Regex(floatPattern);
            if (CalculatorHeight is null || CalculatorHeight == "" || !regex.IsMatch(CalculatorHeight))
                return false;
            if (CalculatorWeight is null || CalculatorWeight == "" || !regex.IsMatch(CalculatorWeight))
                return false;
            return true;
        }
        private void OnCalculateIntakeCommandExecuted(object p)
        {
            Calculator.Age = int.Parse(CalculatorAge);
            Calculator.HeightCm = float.Parse(CalculatorHeight);
            Calculator.WeightKg = float.Parse(CalculatorWeight);

            DailyIntake = Calculator.GetDailyIntake();
            ClearCalculateFormCommand.Execute(p);
        }
        #endregion

        #region Clear Calculate Form
        public ICommand ClearCalculateFormCommand { get; }
        private bool CanClearCalculateFormExecute(object p) => true;
        private void OnClearCalculateFormExecuted(object p)
        {
            CalculatorAge = "";
            CalculatorHeight = "";
            CalculatorWeight = "";
            OnPropertyChanged(nameof(CalculatorAge));
            OnPropertyChanged(nameof(CalculatorHeight));
            OnPropertyChanged(nameof(CalculatorWeight));
        }
        #endregion

        #region Save Current Daily Intake Command
        public ICommand SaveDailyIntake { get; }
        private bool CanSaveDailyIntakeExecute(object p) => DailyIntake is not null;
        private void OnSaveDailyIntakeExecuted(object p)
        {
            CurrentDailyIntake = DailyIntake;
            DailyIntake = null;
        }
        #endregion

        #region Add Dish To Meal
        public ICommand AddDishCommand { get; }
        private bool CanAddDishCommandExecute(object p) => SelectedDish != null;
        private void OnAddDishCommandExecuted(object p)
        {
            SelectedDishes.Add(SelectedDish);
            var temp = SelectedDishes;
            SelectedDishes = new List<Dish>();
            SelectedDishes = temp;
            Meal = new Meal();
            Meal.Dishes = SelectedDishes;
            OnPropertyChanged(nameof(Meal));
        }
        #endregion

        #region Clear Selected Dishes Command
        public ICommand ClearSelectedDishesCommand { get; }
        private bool CanClearSelectedDishesCommandExecute(object p) => SelectedDishes.Count > 0;
        private void OnClearSelectedDishesCommandExecuted(object p)
        {
            SelectedDishes = new List<Dish>();
            Meal = new Meal();
            OnPropertyChanged(nameof(Meal));
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
            CreateDishCommand = new RelayCommand(OnCreateDishCommandExecuted,
                CanCreateDishCommandExecute);
            ClearAddFormCommand = new RelayCommand(OnClearAddFormCommandExecuted, 
                CanClearAddFormCommandExecute);
            CalculateIntakeCommand = new RelayCommand(OnCalculateIntakeCommandExecuted,
                CanCalculateIntakeCommandExecute);
            ClearCalculateFormCommand = new RelayCommand(OnClearCalculateFormExecuted,
                CanClearCalculateFormExecute);
            SaveDailyIntake = new RelayCommand(OnSaveDailyIntakeExecuted,
                CanSaveDailyIntakeExecute);
            AddDishCommand = new RelayCommand(OnAddDishCommandExecuted,
                CanAddDishCommandExecute);
            ClearSelectedDishesCommand = new RelayCommand(OnClearSelectedDishesCommandExecuted, 
                CanClearSelectedDishesCommandExecute);
            #endregion

            Calculator = new Calculator();
            _todayIntake = new DailyIntake();
            _currentDailyIntake = new DailyIntake(2500f, 120f, 90f, 160f);

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
            Dish cucember = new Dish()
            {
                Name = "Cucember",
                Calories = 33.5f,
                Proteins = 4f,
                Fats = 0.6f,
                Carbohydrates = 7.0f
            };
            Dish peach = new Dish()
            {
                Name = "Peach",
                Calories = 150.5f,
                Proteins = 9f,
                Fats = 4.6f,
                Carbohydrates = 65.0f
            };

            Meal meal1 = new Meal();
            meal1.Dishes = new Dish[] { tomato, egg };
            meal1.Type = MealType.Breakfast;

            Meal meal2 = new Meal();
            meal2.Dishes = new Dish[] { chicken, tomato };
            meal2.Type = MealType.Lunch;

            Meal meal3 = new Meal();
            meal3.Dishes = new Dish[] { chicken, tomato, pasta };
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

            Dish[] Prds = new Dish[] { tomato, egg, chicken, pasta, cucember, peach };
            DayRation[] DRArr = new DayRation[] { day1, day2, day3 };
            DayRations = new ObservableCollection<DayRation>(DRArr.OrderBy(x => x.Date));
            Dishes = new List<Dish>(Prds.OrderBy(x => x.Name));  
            SelectedDishes = new List<Dish>();
        }
    }
}
