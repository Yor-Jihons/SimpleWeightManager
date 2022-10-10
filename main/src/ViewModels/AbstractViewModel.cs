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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleWeightManager
{
    namespace ViewModels
    {
        public class AbstractViewModel : System.ComponentModel.INotifyPropertyChanged
        {
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = null;
            protected void NotifyPropertyChanged( string propertyName )
            {
                if( PropertyChanged != null )
                {
                    this.PropertyChanged?.Invoke( this, new System.ComponentModel.PropertyChangedEventArgs(propertyName) );
                }
            }
        }
    }
}
