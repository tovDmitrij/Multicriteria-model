using System.Collections.Generic;
using System.Windows.Controls;
namespace Multicriteria_model
{
    /// <summary>
    /// Вывод результата на форму
    /// </summary>
    public partial class Results : Page
    {
        private readonly string error = "\nТовары отсутствуют!\n";
        /// <summary>
        /// Выводит результат на форму
        /// </summary>
        /// <param name="results">Список результатов каждого способа решения</param>
        public Results(List<string> results)
        {
            InitializeComponent();
            resultLex.Text = results[0] == "" ? error : $"\n{results[0]}";
            resultSub.Text = results[1] == "" ? error : $"\n{results[1]}";
            resultLowBorCr.Text = results[2] == "" ? error : $"\n{results[2]}";
            resultGenCr.Text = results[3] == "" ? error : $"\n{results[3]}";
            resultParOpt.Text = results[4] == "" ? error : $"\n{results[4]}";
        }
    }
}