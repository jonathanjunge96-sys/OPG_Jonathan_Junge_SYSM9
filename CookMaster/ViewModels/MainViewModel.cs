using CookMaster.Managers;
using CookMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICommand? LogIn { get; set; }
        private readonly UserManager? _userManager;
        public User? LoggedInUser { get; private set; } //egenskaper för niloggad användare


        public MainViewModel()
        {
            _userManager = new UserManager();

            LogIn = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void ExecuteLogin()
        {
            var user = _userManager.LoginUser(Username!, Password!);
            if (user != null)
            {
                LoggedInUser = user;
                // Navigera till receptfönster
            }
            else
            {
                MessageBox.Show("Fel användarnamn eller lösenord.");
            }

        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }

}

