using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.ViewModel;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View
{
    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();
            RoleViewModel vmRole = new RoleViewModel();
            CbRole.ItemsSource = vmRole.ListRole;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void tbBirthday_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbBirthday.Visibility == Visibility.Hidden)
            {
                ClBirthday.Visibility = Visibility.Visible;
            }
            else
            {
                ClBirthday.Visibility = Visibility.Hidden;
            }
        }
    }
}
