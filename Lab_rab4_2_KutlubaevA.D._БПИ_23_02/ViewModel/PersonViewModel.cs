using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Helper;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private PersonDPO selectedPersonDpo;
        public PersonDPO SelectedPersonDpo
        {
            get => selectedPersonDpo;
            set { selectedPersonDpo = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Person> ListPerson { get; set; } = new ObservableCollection<Person>();
        public ObservableCollection<PersonDPO> ListPersonDpo { get; set; } = new ObservableCollection<PersonDPO>();

        public PersonViewModel()
        {
            this.ListPerson.Add(new Person { Id = 1, RoleId = 1, FirstName = "Иван", LastName = "Иванов", Birthday = new DateTime(1980, 02, 28) });
            this.ListPerson.Add(new Person { Id = 2, RoleId = 2, FirstName = "Петр", LastName = "Петров", Birthday = new DateTime(1981, 03, 20) });
            this.ListPerson.Add(new Person { Id = 3, RoleId = 3, FirstName = "Виктор", LastName = "Викторов", Birthday = new DateTime(1982, 04, 15) });
            this.ListPerson.Add(new Person { Id = 4, RoleId = 3, FirstName = "Сидор", LastName = "Сидоров", Birthday = new DateTime(1983, 05, 10) });

            ListPersonDpo = GetListPersonDpo();
        }

        public ObservableCollection<PersonDPO> GetListPersonDpo()
        {
            var dpoList = new ObservableCollection<PersonDPO>();
            RoleViewModel vmRole = new RoleViewModel();
            foreach (var person in ListPerson)
            {
                PersonDPO p = new PersonDPO();
                p.CopyFromPerson(person, vmRole.ListRole);
                dpoList.Add(p);
            }
            return dpoList;
        }

        public int MaxId()
        {
            if (!this.ListPerson.Any()) return 0;
            return this.ListPerson.Max(p => p.Id);
        }

        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ?? (addPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee
                    {
                        Title = "Новый сотрудник",
                        Owner = Application.Current?.MainWindow
                    };
                    int maxIdPerson = MaxId() + 1;
                    PersonDPO per = new PersonDPO { Id = maxIdPerson, Birthday = DateTime.Now };
                    wnPerson.DataContext = per;

                    if (wnPerson.ShowDialog() == true)
                    {
                        var selRole = wnPerson.CbRole.SelectedItem as Role;
                        if (selRole != null)
                        {
                            per.Role = selRole.NameRole;
                            ListPersonDpo.Add(per);

                            Person p = new Person();
                            p.CopyFromPersonDPO(per, selRole);
                            ListPerson.Add(p);
                        }
                        else
                        {
                            MessageBox.Show("Не выбрана роль. Сохранение отменено.");
                        }
                    }
                }, (obj) => true));
            }
        }

        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ?? (editPerson = new RelayCommand(obj =>
                {
                    if (SelectedPersonDpo == null) return;

                    PersonDPO tempPerson = SelectedPersonDpo.ShallowCopy();
                    WindowNewEmployee wnPerson = new WindowNewEmployee()
                    {
                        Title = "Редактирование данных сотрудника",
                        DataContext = tempPerson,
                        Owner = Application.Current?.MainWindow
                    };

                    if (wnPerson.ShowDialog() == true)
                    {
                        var selRole = wnPerson.CbRole.SelectedItem as Role;
                        if (selRole != null)
                        {
                            SelectedPersonDpo.Role = selRole.NameRole;
                        }
                        SelectedPersonDpo.FirstName = tempPerson.FirstName;
                        SelectedPersonDpo.LastName = tempPerson.LastName;
                        SelectedPersonDpo.Birthday = tempPerson.Birthday;

                        FindPerson finder = new FindPerson(SelectedPersonDpo.Id);
                        var listPerson = ListPerson.ToList();
                        Person p = listPerson.Find(new Predicate<Person>(finder.PersonPredicate));
                        if (p != null)
                        {
                            RoleViewModel vmRole = new RoleViewModel();
                            var roleObj = vmRole.ListRole.FirstOrDefault(r => r.NameRole == SelectedPersonDpo.Role);
                            p.CopyFromPersonDPO(SelectedPersonDpo, roleObj);
                        }
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ?? (deletePerson = new RelayCommand(obj =>
                {
                    if (SelectedPersonDpo == null) return;

                    PersonDPO person = SelectedPersonDpo;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: \n" + person.LastName + " " + person.FirstName,
                        "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListPersonDpo.Remove(person);
                        Person per = new Person();
                        per.CopyFromPersonDPO(person, null);
                        var toRemove = ListPerson.FirstOrDefault(x => x.Id == person.Id);
                        if (toRemove != null) ListPerson.Remove(toRemove);
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}