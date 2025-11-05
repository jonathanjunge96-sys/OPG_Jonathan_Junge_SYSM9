using System.ComponentModel;

namespace CookMaster.Models
{
    public class ObservableObjectBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}