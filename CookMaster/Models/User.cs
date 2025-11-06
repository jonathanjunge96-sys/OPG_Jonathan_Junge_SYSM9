using CookMaster.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string? SecurityAnswer { get; set; }

        public ObservableCollection<Recipe> Recipes { get; set; } = new();
    }


}

public class AdminUser : User
{
    public void RemoveAnyRecipe(Recipe recipe, IEnumerable<User> allUsers)
    {
        foreach (var user in allUsers)
        {
            if (user.Recipes.Contains(recipe))
            {
                user.Recipes.Remove(recipe);
                break; 
            }
        }
    }
}

