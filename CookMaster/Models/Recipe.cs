﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Ingredients { get; set; } 
        public string Instructions { get; set; }
        public string Catergory { get; set; } //deklarear strings för receptegenskaper
        public DateTime DateCreated { get; set; } 
        public User Author { get; set; }    

        public void EditRecipe(string name, string ingredients, string instructions, string category)  //offentlig metod som ta emot fyra parametrar
        { 
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
            Catergory = category; //uppdaterar innehåll
        }

        public Recipe CopyRecipe() //offentlig metod som returnerar nytt recept
        {
            return new Recipe //ny instans där jag sätter värdena
            {
                Name = this.Name + "(Copy)", 
                Ingredients = this.Ingredients,
                Instructions = this.Instructions,
                Catergory = this.Catergory,//kopierar föregående recept och lägger till "copy"
                DateCreated = DateTime.Now, //sätter tid när receptet skapas
                Author = this.Author
            };
        }



        
        
            
    }
}
