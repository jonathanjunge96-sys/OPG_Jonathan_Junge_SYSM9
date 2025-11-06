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
        private readonly User _currentUser;


        public RecipeListViewModel(User currentUser, UserManager userManager)
        {
            _currentUser = currentUser;
            LoggedInUsername = currentUser.Username;
            IsAdmin = currentUser is AdminUser;
            _userManager = userManager;

            AddRecipeCommand = new RelayCommand(OpenAddRecipeWindow);
            RemoveRecipeCommand = new RelayCommand(RemoveSelectedRecipe);
            ShowDetailsCommand = new RelayCommand(OpenRecipeDetails);
            OpenUserCommand = new RelayCommand(OpenUserDetails);
            SignOutCommand = new RelayCommand(SignOut);
            ShowInfoCommand = new RelayCommand(ShowAppInfo);

            LoadRecipes();
        }

        private void LoadRecipes()
        {
            Recipes.Clear();

            foreach (var user in _userManager.Users)
            {
                foreach (var recipe in user.Recipes)
                {
                    Recipes.Add(recipe);
                }
            }
        }



        private void OpenAddRecipeWindow(object obj)
        {
            var addWindow = new AddRecipeWindow(_currentUser, AddRecipeToList);
            addWindow.Show();
        }

        private void AddRecipeToList(Recipe recipe)
        {
            Recipes.Add(recipe); 
        }


        private void OpenRecipeDetails(object obj)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Välj ett recept först.");
                return;
            }

            var detailWindow = new RecipeDetailWindow(SelectedRecipe, _currentUser);
            detailWindow.Show();
        }



        private void RemoveSelectedRecipe(object obj)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Vänligen markera ett recept först.");
                return;
            }

            try
            {
               
                if (IsAdmin)
                {
                    foreach (var user in _userManager.Users)
                    {
                        if (user.Recipes.Contains(SelectedRecipe))
                        {
                            user.Recipes.Remove(SelectedRecipe);
                        }
                    }

                    Recipes.Remove(SelectedRecipe);
                    LoadRecipes();
                    MessageBox.Show("Recept borttaget för alla användare.");
                    return;
                }

                
                if (SelectedRecipe.Author != _currentUser)
                {
                    MessageBox.Show("Du kan endast ta bort dina egna recept.");
                    return;
                }

                _currentUser.Recipes.Remove(SelectedRecipe);
                Recipes.Remove(SelectedRecipe);
                LoadRecipes();
                MessageBox.Show("Ditt recept har tagits bort.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid borttagning: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }










        private void OpenUserDetails(object obj)
        {
            var userWindow = new UserDetailsWindow(_currentUser, _userManager);
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


