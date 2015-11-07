using System;
using System.Windows.Input;

namespace ModelsGenerator.Common
{

    public class ExtendedCommand : ICommand
    {
        private Action _exectueAction;
        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    if (canExecuteChanged != null)
                        canExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        //parametrized Action constructors
        public ExtendedCommand(Action exectueAction, bool canExecute = true)
        {
            this._exectueAction = exectueAction;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }
        void ICommand.Execute(object parameter)
        {
            if (CanExecute)
            {
                _exectueAction.Invoke();
            }
        }
    }

    public class ExtendedCommand<T> : ICommand
    {
        private Action<T> _exectueAction;
        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    if (canExecuteChanged != null)
                        canExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        //parametrized Action constructors
        public ExtendedCommand(Action<T> exectueAction, bool canExecute = true)
        {
            this._exectueAction = exectueAction;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }
        void ICommand.Execute(object parameter)
        {
            if (CanExecute)
            {
                var genericParameter = parameter;

                if (parameter != null && !(parameter is T))
                {
                    try
                    {
                        genericParameter = Convert.ChangeType(parameter, typeof(T), null);
                    }
                    catch
                    {
                        genericParameter = default(T);
                    }
                }

                _exectueAction.Invoke((T)genericParameter);
            }
        }
    }
}
