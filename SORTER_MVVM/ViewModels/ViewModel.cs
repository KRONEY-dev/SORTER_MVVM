using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SORTER_MVVM.ViewModels
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string Property_Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property_Name));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
