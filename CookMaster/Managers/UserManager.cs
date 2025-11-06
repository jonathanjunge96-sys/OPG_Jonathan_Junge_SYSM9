using System;
using System.Collections.Generic;
using System.Linq;
using CookMaster.Models;
using CookMaster.ViewModels;

namespace CookMaster.Managers
{
    public class UserManager : ObservableObject
    {
        public List<User> Users => _users; //lista med användare

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
            user.Recipes.Add(new Recipe //förprogrammerat recept
            {
                Name = "Stuvade makaroner",
                Ingredients = "Mjölk, makaroner, muskotnöt",
                Category = "Middag",
                DateCreated = DateTime.Now,
                Author = user
            });
            _users.Add(user);

            var admin = new AdminUser //förprogrammerad admin
            {
                Username = "admin",
                Password = "admin",
                Country = "Sweden"
            };
            _users.Add(admin);
        }

        public User? LoginUser(string username, string password)
        {
            Console.WriteLine($"[Login] Försöker logga in med: {username} / {password}");
            foreach (var u in _users)
            {
                Console.WriteLine($"[Login] Finns användare: {u.Username} / {u.Password}");
            }

            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine($"[Login] Inloggning lyckades för: {user.Username}");
            }
            else
            {
                Console.WriteLine("[Login] Inloggning misslyckades.");
            }

            return user;
        }

        public bool Register(string username, string password, string country, string securityAnswer)
        {
            if (_users.Any(u => u.Username == username))
            {
                Console.WriteLine($"[Register] Användarnamnet '{username}' är redan upptaget.");
                return false;
            }

            var newUser = new User
            {
                Username = username,
                Password = password,
                Country = country,
                SecurityAnswer = securityAnswer
            };
            
            _users.Add(newUser);

            Console.WriteLine($"[Register] Registrerad ny användare: {newUser.Username} / {newUser.Password}");
            Console.WriteLine($"[Register] Totalt antal användare: {_users.Count}");

            return true;

           


        }
    }
}
