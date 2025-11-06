using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CookMaster.Models;

namespace CookMaster.ViewModels
{
    public class RecipeDetailViewModel : ObservableObject
    {
        public Recipe Recipe { get; }

        public RecipeDetailViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }
    }

}


