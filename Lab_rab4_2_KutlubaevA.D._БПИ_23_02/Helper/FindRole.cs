using System.Windows;
using Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model;
namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Helper
{
    public class FindRole
    {
        int id;
        public FindRole(int id)
        {
            this.id = id;
        }
        public bool RolePredicate(Role role)
        {
            return role.Id == id;
        }
    }
}
