using CookMaster.Managers;
using CookMaster.Models;
using CookMaster.ViewModels;
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

namespace CookMaster.Views
{
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow(User currentUser, UserManager userManager)
        {
            InitializeComponent();
            var vm = new UserDetailsViewModel(currentUser, userManager);
            vm.CloseAction = () => this.Close();
            DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm)
            {
                vm.NewPassword = (sender as PasswordBox)?.Password;
            }
        }

        private void ConfirmBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm)
            {
                vm.ConfirmPassword = (sender as PasswordBox)?.Password;
            }
        }
    }
}


