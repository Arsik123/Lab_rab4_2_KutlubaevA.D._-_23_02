using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Helper
{
    public class FindPerson
    {
        int id;
        public FindPerson(int id)
        {
            this.id = id;
        }
        public bool PersonPredicate(Person person)
        {
            return person.Id == id;
        }
    }
}
