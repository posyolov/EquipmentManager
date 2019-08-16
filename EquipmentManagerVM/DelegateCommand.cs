using System;
using System.Windows.Input;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Класс для привязки команды от View
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parametr)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((parametr == null) ? default(T) : (T)Convert.ChangeType(parametr, typeof(T)));
        }

        public void Execute(object parametr)
        {
            _execute((parametr == null) ? default(T) : (T)Convert.ChangeType(parametr, typeof(T)));
        }

        public event EventHandler CanExecuteChanged;
        public void RiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}