using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model
{

    public class PersonDPO : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string role;
        public string Role
        {
            get => role;
            set { role = value; OnPropertyChanged(); }
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

        private DateTime birthday;
        public DateTime Birthday
        {
            get => birthday;
            set { birthday = value; OnPropertyChanged(); }
        }

        public PersonDPO() { }
        public PersonDPO(int id, string role, string firstName, string lastName, DateTime birthday)
        {
            this.Id = id; this.Role = role;
            this.FirstName = firstName; this.LastName = lastName; this.Birthday = birthday;
        }

        public PersonDPO ShallowCopy()
        {
            return (PersonDPO)this.MemberwiseClone();
        }

        public PersonDPO CopyFromPerson(Person person, System.Collections.Generic.IEnumerable<Role> roles)
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
                this.Role = roleName;
                this.FirstName = person.FirstName;
                this.LastName = person.LastName;
                this.Birthday = person.Birthday;
            }
            return this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
