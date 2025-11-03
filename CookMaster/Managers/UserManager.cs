using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.ComponentModel;
using CookMaster.Models;
using CookMaster.ViewModels;

namespace CookMaster.Managers
{
    public class UserManager : ObservableObject
    {
        private readonly List<User> _users = new List<User>();
        private User? _currentUser;

        public User? CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public UserManager()
        {
            var user = new User
            {
                Username = "Jonte",
                Password = "1234",
                Country = "Sweden"
            };
            user.Recipes.Add(new Recipe
            {
                Name = "Stuvade makaroner",
                Ingredients = "Mjölk, makaroner, muskotnöt",
                Category = "Middag",
                DateCreated = DateTime.Now,
                Author = user
            });
            _users.Add(user);

            var admin = new AdminUser
            {
                Username = "admin",
                Password = "admin",
                Country = "Sweden"
            };
            _users.Add(admin);
        }

        public User? LoginUser(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
            }
            return user;
        }

        public bool Register(string username, string password, string country)
        {
            if (_users.Any(u => u.Username == username))
                return false;

            var newUser = new User
            {
                Username = username,
                Password = password,
                Country = country
            };

            _users.Add(newUser);
            return true;
        }
    }


}

















