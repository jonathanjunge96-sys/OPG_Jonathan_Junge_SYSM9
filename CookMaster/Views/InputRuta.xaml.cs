using System.Windows;
using System.Windows.Controls;

namespace CookMaster.Views
{
    /// <summary>
    /// Interaction logic for InputRuta.xaml
    /// </summary>
    public partial class InputRuta : UserControl
    {
        public InputRuta()
        {
            InitializeComponent();
        }
        private string placeholder;
        public string Placeholder 
        { 
            get { return placeholder; }
            set 
            { 
                placeholder = value; 
                tbInput.Text = placeholder; 
            }    
        }
        


        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            inputBtn.Clear();
            inputBtn.Focus();

        }

        private void inputBtn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputBtn.Text))
                tbInput.Visibility = Visibility.Visible;
            else
                tbInput.Visibility = Visibility.Hidden;
        }


    }
}
