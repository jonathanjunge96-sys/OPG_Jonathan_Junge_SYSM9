using CookMaster.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            set { _newUsername = value; OnPropertyChanged(nameof(NewUsername)); }
        }

        private string? _newPassword;
        public string? NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        private string? _newCountry;
        public string? NewCountry
        {
            get => _newCountry;
            set { _newCountry = value; OnPropertyChanged(nameof(NewCountry)); }
        }

        private string? _securityAnswer;
        public string? SecurityAnswer
        {
            get => _securityAnswer;
            set { _securityAnswer = value; OnPropertyChanged(nameof(SecurityAnswer)); }
        }

        public ICommand RegisterCommand { get; }

        private readonly UserManager _userManager;

        public RegisterViewModel()
        {
            _userManager = new UserManager();
            RegisterCommand = new RelayCommand(RegisterUser);
        }

        private void RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(NewUsername) ||
                string.IsNullOrWhiteSpace(NewPassword) ||
                string.IsNullOrWhiteSpace(NewCountry) ||
                string.IsNullOrWhiteSpace(SecurityAnswer))
            {
                MessageBox.Show("Alla fält måste fyllas i.");
                return;
            }

            bool success = _userManager.Register(NewUsername, NewPassword, NewCountry, SecurityAnswer);

            if (success)
            {
                MessageBox.Show("Registrering lyckades!");
            }
            else
            {
                MessageBox.Show("Användarnamnet är redan upptaget.");
            }
        }
    }
}

