using CookMaster.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CookMaster.Views;
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

        private string? _confirmPassword;
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        private string _newCountry;
        public string? NewCountry
        {
            get => _newCountry;
            set { _newCountry = value; OnPropertyChanged(nameof(NewCountry)); }
        }

        public List<string> Countries { get; } = new List<string>
        {
            "Sverige",
            "Norge",
            "Finland",
            "Danmark",
            "USA",
            "Storbritannien",
            "Tyskland",
            "Frankrike",
            "Spanien",
            "Polen",
            "Grekland",
            "Italien"
        };

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
                string.IsNullOrWhiteSpace(ConfirmPassword) ||
                string.IsNullOrWhiteSpace(NewCountry) ||
                string.IsNullOrWhiteSpace(SecurityAnswer))
            {
                MessageBox.Show("Alla fält måste fyllas i.");
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Lösenorden matchar inte.");
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
                //tillbaka till mainwindow
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // stäng Registerwindow
                Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)?.Close();

            }
            else
            {
                MessageBox.Show("Användarnamnet är redan upptaget.");
            }
        }

        private bool IsPasswordValid(string password) //valideringsmetod
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password.Length < 8) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;:'\",.<>?/".Contains(ch))) return false;

            return true;
        }
    }
}


