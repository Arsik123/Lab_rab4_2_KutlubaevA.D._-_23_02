using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Newtonsoft.Json;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Helper;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.View;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        string path = Path.Combine(AppContext.BaseDirectory, "DataModels", "PersonData.json");
        private PersonDpo _selectedPersonDpo;
        public PersonDpo SelectedPersonDpo
        {
            get { return _selectedPersonDpo; }
            set
            {
                _selectedPersonDpo = value;
                OnPropertyChanged("SelectedPersonDpo");
            }
        }
        public ObservableCollection<Person> ListPerson { get; set; }
        public ObservableCollection<PersonDpo> ListPersonDpo { get; set; }
        string _jsonPersons = String.Empty;
        public string Error { get; set; }
        public string Message { get; set; }
        public PersonViewModel()
        {
            ListPerson = new ObservableCollection<Person>();
            ListPersonDpo = new ObservableCollection<PersonDpo>();
            ListPerson = LoadPerson();
            ListPersonDpo = GetListPersonDpo();
        }
        private RelayCommand _addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return _addPerson ??
                (_addPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee
                    {
                        Title = "Новый сотрудник"
                    };
                    int maxIdPerson = MaxId() + 1;
                    PersonDpo per = new PersonDpo
                    {
                        Id = maxIdPerson,
                        Birthday = DateTime.Now.ToString(),
                    };
                    wnPerson.DataContext = per;
                    if (wnPerson.ShowDialog() == true)
                    {
                        var r = (Role)wnPerson.CbRole.SelectedValue;
                        if (r != null)
                        {
                            per.RoleName = r.NameRole;
                            per.Birthday = PersonDpo.GetStringBirthday(per.Birthday);
                            ListPersonDpo.Add(per);
                            Person p = new Person();
                            p = p.CopyFromPersonDpo(per);
                            ListPerson.Add(p);
                            try
                            {
                                SaveChanges(ListPerson);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка добавления данных в json файл\\n" + e.Message;
                            }
                        }
                    }
                },
                (obj) => true));
            }
        }
        private RelayCommand _editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return _editPerson ??
                (_editPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee()
                    {
                        Title = "Редактирование данных сотрудника",
                    };
                    PersonDpo personDpo = SelectedPersonDpo;
                    var tempPerson = personDpo.ShallowCopy();
                    wnPerson.DataContext = tempPerson;
                    if (wnPerson.ShowDialog() == true)
                    {
                        var r = (Role)wnPerson.CbRole.SelectedValue;
                        if (r != null)
                        {
                            personDpo.RoleName = r.NameRole;
                            personDpo.FirstName = tempPerson.FirstName;
                            personDpo.LastName = tempPerson.LastName;
                            personDpo.Birthday = PersonDpo.GetStringBirthday(tempPerson.Birthday);
                            var per = ListPerson.FirstOrDefault(p => p.Id == personDpo.Id);
                            if (per != null)
                            {
                                per = per.CopyFromPersonDpo(personDpo);
                            }
                            try
                            {
                                SaveChanges(ListPerson);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка редактирования данных в json файл\\n" + e.Message;
                            }
                        }
                        else
                        {
                            Message = "Необходимо выбрать должность сотрудника.";
                        }
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        private RelayCommand _deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return _deletePerson ??
                (_deletePerson = new RelayCommand(obj =>
                {
                    PersonDpo person = SelectedPersonDpo;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: \\n" +
                    person.LastName + " " + person.FirstName,
                    "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        try
                        {
                            ListPersonDpo.Remove(person);
                            var per = ListPerson.FirstOrDefault(p => p.Id == person.Id);
                            if (per != null)
                            {
                                ListPerson.Remove(per);
                                SaveChanges(ListPerson);
                            }
                        }
                        catch (Exception e)
                        {
                            Error = "Ошибка удаления данных\\n" + e.Message;
                        }
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        public ObservableCollection<Person> LoadPerson()
        {
            _jsonPersons = File.ReadAllText(path);
            if (_jsonPersons != null)
            {
                ListPerson = JsonConvert.DeserializeObject<ObservableCollection<Person>>(_jsonPersons);
                return ListPerson;
            }
            else
            {
                return null;
            }
        }
        public ObservableCollection<PersonDpo> GetListPersonDpo()
        {
            RoleViewModel vmRole = new RoleViewModel();
            foreach (var person in ListPerson)
            {
                PersonDpo p = new PersonDpo();
                p = p.CopyFromPerson(person, vmRole.ListRole);
                ListPersonDpo.Add(p);
            }
            return ListPersonDpo;
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPerson)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
        }
        private void SaveChanges(ObservableCollection<Person> listPersons)
        {
            var jsonPerson = JsonConvert.SerializeObject(listPersons);
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonPerson);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
