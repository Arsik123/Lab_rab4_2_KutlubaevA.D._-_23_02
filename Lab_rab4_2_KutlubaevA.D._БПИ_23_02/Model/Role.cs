using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_rab4_2_KutlubaevA.D._БПИ_23_02.Model
{
    public class Role : INotifyPropertyChanged
    {
        private int id;
        private string nameRole;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string NameRole
        {
            get => nameRole;
            set
            {
                nameRole = value;
                OnPropertyChanged();
            }
        }

        public Role() { }
        public Role(int id, string nameRole)
        {
            this.Id = id;
            this.NameRole = nameRole;
        }

        public Role ShallowCopy()
        {
            return (Role)this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
