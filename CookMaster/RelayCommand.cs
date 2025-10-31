using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookMaster
{
    public class RelayCommand : ICommand //kopplar knapp i UI till metoder in ViewModel
    {
        private readonly Action _execute; //returnerar metod utan värde
        private readonly Func<bool> _canExecute; //returnerar bool
        public RelayCommand(Action execute, Func<bool>? canExecute = null) //kontruktor med villkorskoll
        { 
            _execute = execute;
            _canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public bool CanExecute(object? parameter) //true om det får köras
        {
            return _canExecute == null || _canExecute();
        }

        
        public void Execute(object? parameter)
        {
            _execute();
        }
    }
}
        



        




        
