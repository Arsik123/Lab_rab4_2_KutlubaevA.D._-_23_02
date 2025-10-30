using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View
{
    public partial class WindowNewRole : Window
    {
        public WindowNewRole()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // В методичке диалог просто закрывается с DialogResult = true при сохранении
            this.DialogResult = true;
        }
    }
}
