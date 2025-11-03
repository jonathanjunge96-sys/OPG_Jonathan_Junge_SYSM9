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

    public class UserManager : INotifyPropertyChanged //Hanterar inlog, utlog, reg, currentusr, och meddelar UI om ändringar
    {
        private readonly List<User> _users = new List<User>(); //lista som lagrar användare, ska bara gå att lägga till för användaren

        private User? _currentUser;

        public User? CurrentUser  //returnerar från _currentuser och görs läsbar från resten av programmet
        {
            get => _currentUser;
            private set
            {
                _currentUser = value; //sparar nytt värde u user om anropat
                OnPropertyChanged(nameof(CurrentUser)); //meddelar UI
            }
        }

        public User? LoginUser(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user; // Meddelar UI via OnPropertyChanged
            }
            return user;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Register(string username, string password, string country)
        {
            if (_users.Any(u => u.Username == username))
                return false; //kontroll så inte användarnamnet är upptaget

            var newUser = new User //skapar användare med rätt egenskaper som finns i USer.cs
            {
                Username = username,
                Password = password,
                Country = country
            };

            _users.Add(newUser); //´lägger till i listan
            return true; //lyckat
        }
    }
}

















