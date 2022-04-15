using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenEye.ViewModel.Command
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _predicate;

        public RelayCommand(Action<object> execute,Predicate<object> predicate)
        {
            _execute = execute;
            _predicate = predicate;
        }


        public event EventHandler CanExecuteChanged
        {
             add{
                CommandManager.RequerySuggested += value;
            }
            remove{
                CommandManager.RequerySuggested -= value;
            }

        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null? true: CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
