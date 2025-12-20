using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged; // Fody

namespace PujcovnaApp.ViewModels
{
    // Toto je základ pro všechny ViewModely
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}