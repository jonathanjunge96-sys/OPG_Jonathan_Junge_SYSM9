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

         

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
