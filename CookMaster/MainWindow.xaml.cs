using CookMaster.ViewModels;
using CookMaster.Views;
using System.Windows;
using System.Windows.Controls;

namespace CookMaster
{
    public partial class MainWindow : Window
    {
        private LogInViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new LogInViewModel(); // ✅ rätt ViewModel
            DataContext = _viewModel;
        }

        // Kopplar lösenord från PasswordBox till LogInViewModel
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LogInViewModel vm && sender is PasswordBox pb)
            {
                vm.Password = pb.Password;
            }
        }

        // Öppnar registreringsfönstret med global UserManager
        private void Registre_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(App.GlobalUserManager);
            registerWindow.Show();
            this.Close();
        }
    }
}
