﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Multicriteria_model.pages.settings;

namespace Multicriteria_model.pages.criteria
{
    /// <summary>
    /// Логика взаимодействия для Videocard.xaml
    /// </summary>
    public partial class Videocard : Page
    {
        public Videocard()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //settings.Results = new pages.Results();
            //results.Navigate(new pages.Results());
        }
    }
}
