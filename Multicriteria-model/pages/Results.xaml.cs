﻿using System.Collections.Generic;
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
        public Results(string[] results)
        {
            InitializeComponent();
            resultLex.Text = results[0] == null ? error : $"\n{results[0]}";
            resultSub.Text = results[1] == null ? error : $"\n{results[1]}";
            resultLowBorCr.Text = results[2] == null ? error : $"\n{results[2]}";
            resultGenCr.Text = results[3] == null ? error : $"\n{results[3]}";
            resultParOpt.Text = results[4] == null ? error : $"\n{results[4]}";
        }
    }
}