using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model
{

    public class PersonDpo : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string roleName;
        public string RoleName
        {
            get => roleName;
            set { roleName = value; OnPropertyChanged(); }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(); }
        }

        private string birthday;
        public string Birthday
        {
            get => birthday;
            set 
            { 
                birthday = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(BirthdayDate));
            }
        }

        public DateTime? BirthdayDate
        {
            get
            {
                if (string.IsNullOrEmpty(Birthday))
                    return null;
                
                if (DateTime.TryParseExact(Birthday, "dd.MM.yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
                
                if (DateTime.TryParse(Birthday, out DateTime result2))
                {
                    return result2;
                }
                
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    Birthday = value.Value.ToString("dd.MM.yyyy");
                }
                else
                {
                    Birthday = string.Empty;
                }
                OnPropertyChanged();
            }
        }

        public PersonDpo() { }
        public PersonDpo(int id, string roleName, string firstName, string lastName, string birthday)
        {
            this.Id = id; this.RoleName = roleName;
            this.FirstName = firstName; this.LastName = lastName; this.Birthday = birthday;
        }

        public PersonDpo ShallowCopy()
        {
            return (PersonDpo)this.MemberwiseClone();
        }

        public PersonDpo CopyFromPerson(Person person, System.Collections.Generic.IEnumerable<Role> roles)
        {
            string roleName = string.Empty;
            foreach (var r in roles)
            {
                if (r.Id == person.RoleId)
                {
                    roleName = r.NameRole; break;
                }
            }
            if (!string.IsNullOrEmpty(roleName))
            {
                this.Id = person.Id;
                this.RoleName = roleName;
                this.FirstName = person.FirstName;
                this.LastName = person.LastName;
                this.Birthday = person.Birthday;
            }
            return this;
        }

        public static string GetStringBirthday(string birthday)
        {
            if (DateTime.TryParse(birthday, out DateTime result))
            {
                return result.ToString("dd.MM.yyyy");
            }
            return birthday;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
