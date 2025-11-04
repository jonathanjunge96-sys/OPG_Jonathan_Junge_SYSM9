using System.Windows;
using System.Windows.Controls;
using CookMaster.ViewModels;

namespace CookMaster.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel vm && sender is PasswordBox pb)
            {
                vm.NewPassword = pb.Password;
            }
        }
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) //dubbelkontroll på lösenord
        {
            if (DataContext is RegisterViewModel vm && sender is PasswordBox pb)
            {
                vm.ConfirmPassword = pb.Password;
            }
        }

    }
}


