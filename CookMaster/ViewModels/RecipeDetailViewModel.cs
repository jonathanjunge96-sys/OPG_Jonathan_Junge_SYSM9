using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CookMaster.Models;

namespace CookMaster.ViewModels
{
    public class RecipeDetailViewModel : ObservableObject
    {
        private readonly User _currentUser; //användaren som är inloggad
        private readonly Recipe _recipe; //receptet som visas

        public string Title //titel på receptet
        {
            get => _recipe.Name;
            set { _recipe.Name = value; OnPropertyChanged(); }
        }

        public string Description //beskrivning av receptet
        {
            get => _recipe.Description;
            set { _recipe.Description = value; OnPropertyChanged(); }
        }

        public string Instructions //instruktioner för tillagning
        {
            get => _recipe.Instructions;
            set { _recipe.Instructions = value; OnPropertyChanged(); }
        }

        public string Category //kategori för receptet
        {
            get => _recipe.Category;
            set { _recipe.Category = value; OnPropertyChanged(); }
        }

        public DateTime DateCreated => _recipe.DateCreated; //datum då receptet skapades

        public string AuthorName => _recipe.Author?.Username ?? "Okänd"; //visar användarnamn på författaren

        public ObservableCollection<string> Ingredients { get; set; } //lista med ingredienser

        private string _newIngredient; //ny ingrediens som ska läggas till
        public string NewIngredient
        {
            get => _newIngredient;
            set { _newIngredient = value; OnPropertyChanged(); }
        }

        public ICommand AddIngredientCommand { get; } //kommando för att lägga till ingrediens
        public ICommand RemoveIngredientCommand { get; } //kommando för att ta bort ingrediens
        public ICommand SaveCommand { get; } //kommando för att spara ändringar

        public Action CloseAction { get; set; } //action för att stänga fönstret

        public RecipeDetailViewModel(Recipe recipe, User currentUser) //konstruktor som tar emot recept och användare
        {
            _recipe = recipe;
            _currentUser = currentUser;

            Ingredients = new ObservableCollection<string>(_recipe.Ingredients); //kopierar ingredienser till ObservableCollection

            AddIngredientCommand = new RelayCommand(AddIngredient); //initierar kommandon
            RemoveIngredientCommand = new RelayCommand(RemoveSelectedIngredient);
            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void AddIngredient(object obj) //metod för att lägga till ingrediens
        {
            if (!string.IsNullOrWhiteSpace(NewIngredient))
            {
                Ingredients.Add(NewIngredient);
                NewIngredient = string.Empty;
            }
        }

        private void RemoveSelectedIngredient(object obj) //metod för att ta bort ingrediens
        {
            if (obj is string ingredient && Ingredients.Contains(ingredient))
            {
                Ingredients.Remove(ingredient);
            }
        }

        private void SaveChanges(object obj)
        {
            if (_recipe.Author != _currentUser)
            {
                MessageBox.Show("Du kan endast ändra dina egna recept.");
                return;
            }

            //uppdaterarr ingredienser från textfältet
            var lines = IngredientsText
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            _recipe.Ingredients = lines;

            MessageBox.Show("Receptet har uppdaterats.");
            CloseAction?.Invoke();
        }

        public string IngredientsText
        {
            get => string.Join(Environment.NewLine, Ingredients);
            set
            {
                var lines = value.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Ingredients = new ObservableCollection<string>(lines);
                OnPropertyChanged(); //gör om till string
            }
        }

    }
}