/**
* @file
* @brief The abstract class for the View-Model.
*/


namespace SimpleWeightManager.ViewModels
{
    /// <summary>
    /// The abstract class for the View-Model.
    /// </summary>
    public class AbstractViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Event Handler.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = null;

        /// <summary>
        /// Notify that propertyName is changed.
        /// </summary>
        /// <param name="propertyName">The string which means a property.</param>
        protected void NotifyPropertyChanged( string propertyName )
        {
            if( PropertyChanged != null )
            {
                this.PropertyChanged?.Invoke( this, new System.ComponentModel.PropertyChangedEventArgs(propertyName) );
            }
        }
    }
}
