using CookMaster.Managers;
using CookMaster.Models;
using CookMaster.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string? _username;
        public string? Username //egenskap för användarnamn
        {
            get => _username;
            set
            {
                _username = value; //uppdaterar värde från UI
                OnPropertyChanged(nameof(Username)); //meddelar UI
            }
        }

        private string? _password;
        public string? Password //egenskap för lösenord
        {
            get => _password;
            set
            {
                _password = value; //uppdaterar värde från UI
                OnPropertyChanged(nameof(Password)); //meddelar UI
            }
        }

        public ICommand? LogIn { get; set; }
        private readonly UserManager _userManager;
        public User? LoggedInUser { get; private set; } //egenskaper för niloggad användare

        public MainViewModel()
        {
            _userManager = new UserManager();
            LogIn = new RelayCommand(ExecuteLogin);
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
                LoggedInUser = user;
                MessageBox.Show($"Inloggning lyckades! Välkommen {user.Username}");
                // Öppna RecipeWindow
                var recipeWindow = new RecipeListWindow(user, _userManager);
                recipeWindow.Show();

                // Stäng MainWindow
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Fel användarnamn eller lösenord.");
            }
        }

    }



}

