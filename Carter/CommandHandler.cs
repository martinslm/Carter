using System;
using System.Windows.Input;

namespace Carter
{
    class CommandHandler : ICommand
    {
        readonly Action<object> _action;
        readonly Predicate<object> _canExecute;

        public CommandHandler(Action<object> action, Predicate<object> canExecute)
        {
            if (_action == null)
                new ArgumentNullException("Action indefined");
            _action = action;
            _canExecute = canExecute;
        }
        public CommandHandler(Action<object> action)
        {
            if (_action == null)
                new ArgumentNullException("Action indefined");

            _action = action;
            _canExecute = null;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
