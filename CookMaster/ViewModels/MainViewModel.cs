using CookMaster.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICommand? LogIn { get; set; }
        private readonly UserManager? _userManager;

        public MainViewModel()
        {
            _userManager = new UserManager();

            LogIn = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void ExecuteLogin()
        {
            bool success = _userManager.Login(Username!, Password!);
            if (success)
            {
                // Navigera till RecipeListWindow eller visa meddelande
            }
            else
            {
                // Visa felmeddelande i UI
            }
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }

}

