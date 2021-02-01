using Calculator.Windows.Main.ViewModels.Interfaces;

namespace Calculator.Windows.Main
{
    public partial class MainWindow
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }
    }
}
