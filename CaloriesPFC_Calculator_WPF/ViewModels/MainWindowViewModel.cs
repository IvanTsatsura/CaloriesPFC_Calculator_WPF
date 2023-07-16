using CaloriesPFC_Calculator_WPF.Models;
using CaloriesPFC_Calculator_WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
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

        #region All DayRation property : IEnumerable<DayRation>
        public IEnumerable<DayRation>? DayRations { get; set; }
        #endregion

        #region Selected DayRation field + property : DayRation
        private DayRation? _dayRation;
        public DayRation DayRation
        {
            get => _dayRation;
            set => Set(ref _dayRation, value);
        }
        #endregion
    }
}
