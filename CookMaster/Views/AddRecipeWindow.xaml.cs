using CookMaster.ViewModels;
using System.Windows;

namespace CookMaster.Views
{
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            DataContext = new AddRecipeWindowModel();
        }
    }
}

