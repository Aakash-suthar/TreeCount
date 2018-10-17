

using System;
using System.Windows.Input;

namespace tree
{
     class RelayCommand : ICommand
    {
        private Action maction;
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        public RelayCommand(Action a)
        {
            maction = a;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            maction();
        }
    }
}
