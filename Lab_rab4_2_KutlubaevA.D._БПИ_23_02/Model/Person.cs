using System;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public Person() { }
        public Person(int id, int roleId, string firstName, string lastName, DateTime birthday)
        {
            this.Id = id; this.RoleId = roleId;
            this.FirstName = firstName; this.LastName = lastName; this.Birthday = birthday;
        }

        public Person CopyFromPersonDPO(PersonDPO dpo, Role role = null)
        {
            this.Id = dpo.Id;
            this.FirstName = dpo.FirstName;
            this.LastName = dpo.LastName;
            this.Birthday = dpo.Birthday;
            if (role != null) this.RoleId = role.Id;
            return this;
        }
    }
}