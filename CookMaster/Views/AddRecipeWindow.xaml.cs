using CookMaster.Models;
using CookMaster.ViewModels;
using System.Windows;

namespace CookMaster.Views
{
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow(User currentUser)
        {
            InitializeComponent();
            DataContext = new AddRecipeWindowModel(currentUser);
        }
    }
}


