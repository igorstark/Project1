using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMTools
{
    public class DelegateCommand : ICommand
    {
        #region Event

        public event EventHandler CanExecuteChanged
        {
            add
            {
                System.Windows.Input.CommandManager.RequerySuggested += value;
            }
            remove
            {
                System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        ///     Action est une référence sur une méthode qui ne retourne pas de paramètre.
        ///     Dans notre cas, la méthode référencée prendra en paramètre un objet.
        /// </summary>
        private Action<object> _ExecuteReference;

        /// <summary>
        ///     Func est une référence sur une méthode qui retourne un paramètre.
        ///     Dans notre cas, la méthode référencée prendra en paramètre un objet et retourne un booléen.
        /// </summary>
        private Func<object, bool> _CanExecuteReference;

        #endregion

        /// <summary>
        ///     Initialise une nouvelle instance d'une commande.
        /// </summary>
        /// <param name="executeReference">Méthode à appeler lorsque la commande est exécuté.</param>
        /// <param name="canExecuteReference">Méthode à appeler lorsque la commande doit être testé.</param>
        public DelegateCommand(Action<object> executeReference, Func<object, bool> canExecuteReference = null)
        {
            _ExecuteReference = executeReference;
            _CanExecuteReference = canExecuteReference;
        }

        #region Methods

        public bool CanExecute(object parameter)
        {
            if(_CanExecuteReference != null)
            {
                return _CanExecuteReference(parameter);
            }
            //Si on ne précise pas de logique de test d'une commande
            //on considère qu'elle peut être exécutée.
            return true;
        }

        public void Execute(object parameter)
        {
            //Si on référence une méthode
            if (_ExecuteReference != null)
            {
                //On déclenche la méthode référencée.
                _ExecuteReference(parameter);
            }
        }

        #endregion

    }
}
