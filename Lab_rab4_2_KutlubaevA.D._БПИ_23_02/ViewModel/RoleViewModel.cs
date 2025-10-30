using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Helper;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.ViewModel
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        private Role selectedRole;
        public Role SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Role> ListRole { get; set; } = new ObservableCollection<Role>();

        public RoleViewModel()
        {
            this.ListRole.Add(new Role { Id = 1, NameRole = "Директор" });
            this.ListRole.Add(new Role { Id = 2, NameRole = "Бухгалтер" });
            this.ListRole.Add(new Role { Id = 3, NameRole = "Менеджер" });
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListRole)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
            }
            return max;
        }

        private RelayCommand addRole;
        public RelayCommand AddRole
        {
            get
            {
                return addRole ??
                (addRole = new RelayCommand(obj =>
                {
                    WindowNewRole wn = new WindowNewRole
                    {
                        Title = "Новая должность"
                    };

                    Role newRole = new Role
                    {
                        Id = MaxId() + 1,
                        NameRole = string.Empty
                    };

                    wn.DataContext = newRole;
                    if (wn.ShowDialog() == true)
                    {
                        ListRole.Add(newRole);
                    }
                }, (obj) => true));
            }
        }

        private RelayCommand editRole;
        public RelayCommand EditRole
        {
            get
            {
                return editRole ??
                (editRole = new RelayCommand(obj =>
                {
                    WindowNewRole wn = new WindowNewRole
                    {
                        Title = "Редактирование должности"
                    };

                    Role temp = SelectedRole.ShallowCopy();
                    wn.DataContext = temp;

                    if (wn.ShowDialog() == true)
                    {
                        SelectedRole.NameRole = temp.NameRole;
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }

        private RelayCommand deleteRole;
        public RelayCommand DeleteRole
        {
            get
            {
                return deleteRole ??
                (deleteRole = new RelayCommand(obj =>
                {
                    Role role = SelectedRole;
                    MessageBoxResult result = MessageBox.Show("Удалить должность: \n" + role.NameRole,
                        "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListRole.Remove(role);
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]
            string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
