using CookMaster.Managers;
using CookMaster.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    public class UserDetailsViewModel : ObservableObject
    {
        private readonly UserManager _userManager;
        private readonly User _currentUser;

        public string CurrentUsername => _currentUser.Username;
        public string CurrentCountry => _currentUser.Country;

        private string _newUsername;
        public string NewUsername
        {
            get => _newUsername;
            set { _newUsername = value; OnPropertyChanged(); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Countries { get; set; } = new()
        {
            "Sverige", "Norge", "Danmark", "Finland", "Island"
        };

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set { _selectedCountry = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseAction { get; set; }

        public UserDetailsViewModel(User currentUser, UserManager userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;

            NewUsername = currentUser.Username;
            SelectedCountry = currentUser.Country;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveChanges(object obj)
        {
            if (NewUsername.Length < 3)
            {
                MessageBox.Show("Användarnamnet måste vara minst 3 tecken.");
                return;
            }

            if (_userManager.Users.Any(u => u != _currentUser && u.Username == NewUsername))
            {
                MessageBox.Show("Användarnamnet är redan upptaget.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(NewPassword) || !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                if (NewPassword != ConfirmPassword)
                {
                    MessageBox.Show("Lösenorden matchar inte.");
                    return;
                }

                if (NewPassword.Length < 5)
                {
                    MessageBox.Show("Lösenordet måste vara minst 5 tecken.");
                    return;
                }

                _currentUser.Password = NewPassword;
            }

            _currentUser.Username = NewUsername;
            _currentUser.Country = SelectedCountry;

            MessageBox.Show("Användaruppgifter uppdaterade.");
            CloseAction?.Invoke();
        }

        private void Cancel(object obj)
        {
            CloseAction?.Invoke();
        }
    }
}
