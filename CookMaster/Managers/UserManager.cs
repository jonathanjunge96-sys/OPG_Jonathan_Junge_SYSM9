using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.ComponentModel;
using CookMaster.Models;

namespace CookMaster.Managers
{

    public class UserManager : INotifyPropertyChanged 
    {
        private User? _currentUser;
        private readonly List<User> _users = new();
        public UserManager() { }

        public User? CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        public bool IsAuthenticated => CurrentUser != null;

        public bool Login(string username, string password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // För test: Lägg till användare manuellt
        public void SeedUsers()
        {
            _users.Add(new User
            {
                Username = "admin",
                Password = "1234",
                DisplayName = "Admin",
                Role = "Administrator"
            });

            _users.Add(new User
            {
                Username = "jonathan",
                Password = "hemligt",
                DisplayName = "Jonathan",
                Role = "User"
            });
        }
    }
}
