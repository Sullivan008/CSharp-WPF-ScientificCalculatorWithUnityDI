using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.Commands
{
    public class RelayCommandAsync : ICommand
    {
        /// <summary>
        ///     A végrehajtani kívánt szálat tartalmazza.
        /// </summary>
        private readonly Func<Task> _execute;


        /// <summary>
        ///     A végrehajtani kívánt feladatot tartalmazza.
        /// </summary>
        private readonly Action<object> _executeParam;

        /// <summary>
        ///     A végrehajtáshoz szükséges kritériumokat tárolja.
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        ///     Tárolja, hogy a parancs végrehajtás alatt áll-e.
        /// </summary>
        private bool _isExecuting;

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        /// <param name="execute">A végrehajtani kívánt feladat</param>
        public RelayCommandAsync(Func<Task> execute) : this(execute, null) { }


        public RelayCommandAsync(Action<object> executeParam)
        {
            _executeParam = executeParam;
        }

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        /// <param name="execute">A végrehajtani kívánt feladat</param>
        /// <param name="canExecute">A végrehajtáshoz szükséges feltételek</param>
        public RelayCommandAsync(Func<Task> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Eseményekre fel illetve leíratkozás, ezek segítségével lehet egy command végrehajtását automatizálni.
        ///     Ha olyan változás történik az adatokban amely hatással van egy parancs végrehajthatóságával kapcsolatban meghívja a CanExecute metódust.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Meghatározza, hogy a parancs végrehajtható-e.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>
        ///     TRUE - A Command megfelel a feltételeknek, végrehajtható.
        ///     FALSE - A Command nem hajtható végre.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (!_isExecuting && _canExecute == null)
            {
                return true;
            }
            return (!_isExecuting && _canExecute(parameter));
        }

        /// <summary>
        ///     Ha a CanExecute TRUE értékkel tér vissza, végrehajtja a parancsot.
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object parameter)
        {
            _isExecuting = true;

            try
            {
                if (_execute != null)
                {
                    await _execute();
                }
                else
                {
                    _executeParam(parameter);
                }
            }
            finally
            {
                _isExecuting = false;
            }
        }

    }
}
