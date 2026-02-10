using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.ViewModel;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
