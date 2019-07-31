using System.Windows;
using System.Windows.Controls;

namespace Calculator.Views
{
    public partial class CalculatorView : UserControl
    {
        public CalculatorView()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     TextBox átméretezése esetén a FontSize igazítás a TextBox magasságához.
        /// </summary>
        private void TextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ValueTextBox.FontSize = (ValueTextBox.ActualHeight / ValueTextBox.MaxLines) * (0.6);
        }

        /// <summary>
        ///     A UserControl kerüljön Focus-ba, annak érdekében, hogy a XAML-ben definiált KeyBinding-ek
        ///     működjenek.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }
    }
}
