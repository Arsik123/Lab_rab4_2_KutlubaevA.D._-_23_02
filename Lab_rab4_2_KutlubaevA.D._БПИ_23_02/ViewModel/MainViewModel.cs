using System.Windows.Input;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Helper;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.ViewModel
{
    public class MainViewModel
    {
        public RelayCommand OpenEmployeeCommand { get; }
        public RelayCommand OpenRoleCommand { get; }

        public MainViewModel()
        {
            OpenEmployeeCommand = new RelayCommand(OpenEmployee);
            OpenRoleCommand = new RelayCommand(OpenRole);
        }

        private void OpenEmployee(object parameter)
        {
            WindowEmployee wEmployee = new WindowEmployee();
            wEmployee.Show();
        }

        private void OpenRole(object parameter)
        {
            WindowRole wRole = new WindowRole();
            wRole.Show();
        }
    }
}
