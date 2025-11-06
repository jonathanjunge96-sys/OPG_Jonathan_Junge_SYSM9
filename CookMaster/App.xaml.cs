using System.Configuration;
using System.Data;
using System.Windows;
using CookMaster.Managers;

namespace CookMaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserManager GlobalUserManager { get; } = new UserManager();
        public App()
        {
            InitializeComponent();
        }
    }


}
