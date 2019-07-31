using Calculator.Commands;
using Calculator.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public MainViewModel()
        {
            Globals.MainViewModelData = this;
        }

        #region Properties

        #region ViewModels

        private CalculatorViewModel _calculatorViewModelData;
        /// <summary>
        ///     Property, amely a CalculatorViewModel egy példányát tartalmazza.
        ///     Jelen esetben a MainViewModel vezérli a többit, így a CalculatorViewModel-ből egy példány készül.
        ///     Lehetőség van CalculatorView XAML felől is közvetlenü elérni a ViewModel-t ami hozzá tartozik, 
        ///         viszont ebben az esetben ahányszor betöltődik a View annyi új példányt hoz létre. 
        /// </summary>
        public CalculatorViewModel CalculatorViewModelData
        {
            get
            {
                if(_calculatorViewModelData == null)
                {
                    _calculatorViewModelData = new CalculatorViewModel();
                }

                return _calculatorViewModelData;
            }
        }

        #endregion

        #endregion

        #region Commands

        private ICommand _closeCommand;
        /// <summary>
        ///     Command, amely a kilépés gombhoz tartozik.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if(_closeCommand == null)
                {
                    _closeCommand = new RelayCommandAsync(() => Close());
                }

                return _closeCommand;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Metódus, amely az alkalmazás bezárását végzi.
        /// </summary>
        private Task Close()
        {
            System.Windows.Application.Current.Shutdown();

            return Task.FromResult(true);
        }

        #endregion
    }
}
