using CookMaster.Managers;
using CookMaster.Models;
using CookMaster.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    public class RegisterViewModel : ObservableObject
    {
        private string? _newUsername;
        public string? NewUsername
        {
            get => _newUsername;
            set { _newUsername = value; OnPropertyChanged(); }
        }

        private string? _newPassword;
        public string? NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private string? _confirmPassword;
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        private string? _newCountry;
        public string? NewCountry
        {
            get => _newCountry;
            set { _newCountry = value; OnPropertyChanged(); }
        }

        public List<string> Countries { get; } = new List<string>
        {
            "Sverige", "Norge", "Finland", "Danmark", "USA",
            "Storbritannien", "Tyskland", "Frankrike", "Spanien",
            "Polen", "Grekland", "Italien"
        };

        private string? _securityAnswer;
        public string? SecurityAnswer
        {
            get => _securityAnswer;
            set { _securityAnswer = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        private readonly UserManager _userManager;

        public RegisterViewModel(UserManager userManager) 
        {
            _userManager = userManager; 
            RegisterCommand = new RelayCommand(RegisterUser);
        }


        private void RegisterUser(object obj)
        {
            if (string.IsNullOrWhiteSpace(NewUsername) ||
                string.IsNullOrWhiteSpace(NewPassword) ||
                string.IsNullOrWhiteSpace(ConfirmPassword) ||
                string.IsNullOrWhiteSpace(NewCountry) ||
                string.IsNullOrWhiteSpace(SecurityAnswer))
            {
                MessageBox.Show("Alla fält måste fyllas i.");
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Lösenorden stämmer inte överens.");
                return;
            }

            if (!IsPasswordValid(NewPassword))
            {
                MessageBox.Show("Lösenordet måste vara minst 8 tecken långt, innehålla minst en siffra och ett specialtecken.");
                return;
            }

            bool success = _userManager.Register(NewUsername, NewPassword, NewCountry, SecurityAnswer);

            if (success)
            {
                MessageBox.Show("Registrering lyckades!");
                var mainWindow = new MainWindow();
                mainWindow.Show();

                Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            else
            {
                MessageBox.Show("Användarnamnet är redan upptaget.");
            }
        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password.Length < 8) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;:'\",.<>?/".Contains(ch))) return false;

            return true;
        }
    }
}
