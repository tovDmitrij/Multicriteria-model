using System;
using System.Windows.Controls;
namespace Multicriteria_model
{
    /// <summary>
    /// Критерий фильтрации списка товаров
    /// </summary>
    public partial class CriteriaElement : UserControl
    {
        /// <summary>
        /// Наименование критерия
        /// </summary>
        public string Name => criteriaName.Content.ToString();
        /// <summary>
        /// Значение критерия
        /// </summary>
        public string Value => criteriaValue.Text;
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
            criteriaUpperOrLower.Content = Convert.ToInt32(value) >= 0 ? "Не меньше" : "Не больше";
            criteriaName.Content = name;
        }
    }
}