using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View;
namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Employee_OnClick(object sender, RoutedEventArgs e)
        {
            WindowEmployee wEmployee = new WindowEmployee(); wEmployee.Show();
        }
        private void Role_OnClick(object sender, RoutedEventArgs e)
        {
            WindowRole wRole = new WindowRole(); wRole.Show();
        }
    }
}
