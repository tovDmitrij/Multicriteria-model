using System.Windows;
namespace Multicriteria_model
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            task.Navigate(new Task());
        }
    }
}