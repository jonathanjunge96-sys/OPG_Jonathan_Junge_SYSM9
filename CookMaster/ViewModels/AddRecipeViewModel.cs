using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CookMaster.Models;

namespace CookMaster.ViewModels
{
    public class AddRecipeWindowModel : ObservableObject
    {
        // Fält
        private string _name;
        private string _ingredients;
        private string _instructions;
        private string _message;
        private string _selectedCategory;
        private DateTime? _selectedDate;

        // Egenskaper
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Ingredients
        {
            get => _ingredients;
            set { _ingredients = value; OnPropertyChanged(); }
        }

        public string Instructions
        {
            get => _instructions;
            set { _instructions = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Categories { get; set; }

        // Lista med sparade recept
        public static ObservableCollection<Recipe> SavedRecipes { get; set; } = new ObservableCollection<Recipe>();

        // Kommandon
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Konstruktor
        public AddRecipeWindowModel()
        {
            Categories = new ObservableCollection<string>
            {
                "Frukost", "Lunch", "Middag", "Efterrätt", "Snacks"
            };

            SaveCommand = new RelayCommand(SaveRecipe);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Spara recept
        private void SaveRecipe(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Ingredients) ||
                string.IsNullOrWhiteSpace(Instructions) ||
                string.IsNullOrWhiteSpace(SelectedCategory) ||
                SelectedDate == null)
            {
                Message = "Fyll i alla fält!";
                return;
            }

            var newRecipe = new Recipe
            {
                Name = Name,
                Ingredients = Ingredients,
                Instructions = Instructions,
                Category = SelectedCategory,
                DateCreated = SelectedDate.Value,
                Author = null // Du kan sätta aktuell användare här om du har en
            };

            SavedRecipes.Add(newRecipe);
            Message = "Recept sparat!";
            ClearFields();
        }

        // Rensa fälten
        private void ClearFields()
        {
            Name = "";
            Ingredients = "";
            Instructions = "";
            SelectedCategory = null;
            SelectedDate = null;
        }

        // Avbryt
        private void Cancel(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}


