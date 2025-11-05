using CookMaster.Managers;
using CookMaster.Views;
using CookMaster.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    public class LogInViewModel : ObservableObject
    {
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogInCommand { get; }

        private readonly UserManager _userManager;

        public LogInViewModel()
        {
            _userManager = new UserManager();
            LogInCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin(object obj) 
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Fyll i både användarnamn och lösenord.");
                return;
            }

            var user = _userManager.LoginUser(Username, Password);
            if (user != null)
            {
                MessageBox.Show($"Inloggning lyckades! Välkommen {user.Username}");

                var recipeListWindow = new RecipeListWindow(user, _userManager);
                recipeListWindow.Show();

                Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            else
            {
                MessageBox.Show("Fel användarnamn eller lösenord.");
            }
        }
    }
}













