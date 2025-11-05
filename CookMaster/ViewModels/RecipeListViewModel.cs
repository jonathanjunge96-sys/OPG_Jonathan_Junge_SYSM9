using CookMaster.Managers;
using CookMaster.Models;
using CookMaster.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModels
{
    public class RecipeListViewModel : ObservableObject
    {
        public ObservableCollection<Recipe> Recipes { get; set; } = new ObservableCollection<Recipe>();

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set { _selectedRecipe = value; OnPropertyChanged(nameof(SelectedRecipe)); }
        }

        public string LoggedInUsername { get; set; }
        public bool IsAdmin { get; set; }

        // Kommandon
        public ICommand AddRecipeCommand { get; }
        public ICommand RemoveRecipeCommand { get; }
        public ICommand ShowDetailsCommand { get; }
        public ICommand OpenUserCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand ShowInfoCommand { get; }

        private readonly UserManager _userManager;

        public RecipeListViewModel(User currentUser, UserManager userManager)
        {
            LoggedInUsername = currentUser.Username;
            IsAdmin = currentUser is AdminUser;
            _userManager = userManager;

            AddRecipeCommand = new RelayCommand(OpenAddRecipeWindow);
            RemoveRecipeCommand = new RelayCommand(RemoveSelectedRecipe);
            ShowDetailsCommand = new RelayCommand(OpenRecipeDetails);
            OpenUserCommand = new RelayCommand(OpenUserDetails);
            SignOutCommand = new RelayCommand(SignOut);
            ShowInfoCommand = new RelayCommand(ShowAppInfo);

            LoadRecipes(currentUser);
        }

        private void LoadRecipes(User currentUser)
        {
            Recipes.Clear();

            if (IsAdmin)
            {
                foreach (var user in _userManager.Users)
                    foreach (var recipe in user.Recipes)
                        Recipes.Add(recipe);
            }
            else
            {
                foreach (var recipe in currentUser.Recipes)
                    Recipes.Add(recipe);
            }
        }

        private void OpenAddRecipeWindow(object obj)
        {
            var addWindow = new AddRecipeWindow();
            addWindow.Show();
        }

        private void OpenRecipeDetails(object obj)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Vänligen markera ett recept först.");
                return;
            }

            var detailWindow = new RecipeDetailWindow();
            detailWindow.DataContext = SelectedRecipe;
            detailWindow.Show();

        }

        private void RemoveSelectedRecipe(object obj)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Vänligen markera ett recept först.");
                return;
            }

            Recipes.Remove(SelectedRecipe);
            SelectedRecipe.Author.Recipes.Remove(SelectedRecipe);
        }

        private void OpenUserDetails(object obj)
        {
            var userWindow = new UserDetailsWindow();
            userWindow.Show();
        }

        private void SignOut(object obj)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)?.Close();
        }

        private void ShowAppInfo(object obj)
        {
            MessageBox.Show("CookMaster är en receptplattform där du kan spara, visa och dela dina favoriträtter.");
        }
    }
}


