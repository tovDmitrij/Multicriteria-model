using System.Windows.Controls;
namespace Multicriteria_model.pages
{
    /// <summary>
    /// Вывод результата
    /// </summary>
    public partial class Results : Page
    {
        /// <summary>
        /// Вывод результата
        /// </summary>
        /// <param name="shortResult">Краткий результат</param>
        /// <param name="detailedResult">Подробный результат</param>
        public Results(string shortResult, string detailedResult)
        {
            InitializeComponent();
            shortResultText.Text = shortResult;
            detailedResultText.Text = detailedResult;
        }
    }
}