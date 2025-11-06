using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CookMaster.Models
{
    public class Recipe : ObservableObject
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public string Instructions { get; set; }
        public string Category { get; set; } //deklarear strings för receptegenskaper
        public DateTime DateCreated { get; set; }
        public User Author { get; set; }
        public string Description { get; set; } //till edit

        public void EditRecipe(string name, string ingredients, string instructions, string category, string description)  //offentlig metod som ta emot fem parametrar
        {
            Name = name;
            Ingredients = ingredients.Split(',').Select(i => i.Trim()).ToList(); //konverterar till lista
            Instructions = instructions;
            Category = category; //uppdaterar innehåll
            Description = description; //uppdaterar beskrivning
        }

        public Recipe CopyRecipe() //offentlig metod som returnerar nytt recept
        {
            return new Recipe //ny instans där jag sätter värdena
            {
                Name = this.Name + "(Copy)",
                Ingredients = new List<string>(this.Ingredients), //djupkopierar ingredienser
                Instructions = this.Instructions,
                Category = this.Category, //kopierar föregående recept och lägger till "copy"
                Description = this.Description, //kopierar beskrivning
                DateCreated = DateTime.Now, //sätter tid när receptet skapas
                Author = this.Author
            };
        }

        public override string ToString() //gör så att det blir användbart i listboxes
        {
            return $"{Name} ({Category}) - {Author?.Username}";
        }
    }
}