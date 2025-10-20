using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Models
{
    public class User //skapar modellklass för att lagra data
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; }
    }

}
