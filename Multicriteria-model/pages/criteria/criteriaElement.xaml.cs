using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace Multicriteria_model
{
    /// <summary>
    /// Критерий фильтрации списка товаров
    /// </summary>
    public partial class CriteriaElement : UserControl
    {
        private readonly bool _upper;
        /// <summary>
        /// Наименование критерия
        /// </summary>
        public string Name => criteriaName.Content.ToString();
        /// <summary>
        /// Значение критерия
        /// </summary>
        public string Value => criteriaValue.Text;
        /// <summary>
        /// Значение критерия не больше (false) или не меньше (true)
        /// </summary>
        public bool Upper => _upper;
        /// <summary>
        /// Приоритет критерия
        /// </summary>
        public uint Priority => Convert.ToUInt32(criteriaPriority.Text);
        /// <summary>
        /// Критерий фильтрации списка товаров
        /// </summary>
        /// <param name="name">Наименование критерия</param>
        /// <param name="value">Значение критерия</param>
        public CriteriaElement(string name, dynamic value)
        {
            InitializeComponent();
            if (Convert.ToInt32(value) >= 0)
            {
                criteriaUpperOrLower.Content = "Не меньше";
                _upper = true;
            }
            else
            {
                criteriaUpperOrLower.Content = "Не больше";
                _upper= false;
            }
            criteriaName.Content = name;
        }

        private void PreviewPriorityInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        private void PreviewValueInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regexNumber = new Regex("[^0-9]+");
            Regex regexAnotherSymbols = new Regex(",");
            e.Handled = regexAnotherSymbols.IsMatch(e.Text) || regexNumber.IsMatch(e.Text);
        }
    }
}