using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Models
{
    public class User //skapar modellklass för att lagra data
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Country { get; set; }
        public List<Recipe> Recipes { get; set; } = new();

    }

    public class AdminUser : User
    {
        public void RemoveAnyRecipe(Recipe recipe, List<User> allUsers)
        {
            foreach (var user in allUsers)
            {
                if (user.Recipes.Contains(recipe))      
                {
                    user.Recipes.Remove(recipe);
                    break; // Om receptet bara finns hos en användare
                }
            }
        }
    }
}
