using CookMaster.Managers;
using CookMaster.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    public class LogInViewModel : ObservableObject
    {
        private string _username = string.Empty; // lagrar här
        public string Username // tillgänglig utifrån
        {
            get => _username;
            set
            {
                _username = value; // uppdaterar värde från UI
                OnPropertyChanged(nameof(Username)); // meddelar UI
            }
        }

        private string _password = string.Empty; // samma princip som ovan
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LogInCommand { get; }

        private readonly UserManager _userManager;

        public LogInViewModel()
        {
            _userManager = new UserManager();
            LogInCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin()
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

                // Öppna RecipeWindow
                var recipeWindow = new RecipeListWindow();
                recipeWindow.Show();

                // Stäng loginfönstret
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

