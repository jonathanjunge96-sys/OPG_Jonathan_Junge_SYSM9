using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.ViewModels
{
    public class LogInViewModel : ObservableObject
    {
        

        private string _username = string.Empty; //lagrar här
        public string Username //tillgägnlig utifrån
        { 

            get 
            { 
                return _username; //returnera värdet för studen
            }
            set
            {
                _username = value; //uppdaterar värde från UI
                OnPropertyChanged(nameof(Username)); //meddelear UI
            } 
        }

        private string _password = string.Empty; //samma princip som ovan
        public string Password 
        {
            get 
            { 
                return _password;
            }
            set 
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        
        }

       

                
             
        


        
    }
}
