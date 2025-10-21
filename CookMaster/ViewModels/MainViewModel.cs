using CookMaster.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    internal class MainViewModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICommand? LogIn { get; set; }
        private readonly UserManager? _userManager;  
    }
}
