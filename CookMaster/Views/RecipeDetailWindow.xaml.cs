using CookMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CookMaster.ViewModels;


namespace CookMaster.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        private Recipe _recipe;

        public RecipeDetailWindow(Recipe recipe)
        {
            InitializeComponent();
            _recipe = recipe;

            DataContext = _recipe;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }



}
