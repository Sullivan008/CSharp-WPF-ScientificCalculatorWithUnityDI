using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculator.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Metódus, amely meghívja a PropertyChanged eseményt, amely a felületnek jelzi,
        ///         hogy egy Property értéke megváltozott.
        /// </summary>
        /// <param name="name">Property neve,a melynek az értéke megváltozott.</param>
        public void OnPropertyChanged([CallerMemberName()] string name = null)
        {
            if (name != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
