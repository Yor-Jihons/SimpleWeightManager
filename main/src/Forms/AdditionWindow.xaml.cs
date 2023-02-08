/**
* @file
* @brief The AdditionWindow.
*/

using System.Windows;
using System.Windows.Input;

namespace SimpleWeightManager
{
    /// <summary>
    /// Interaction logic for AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModel">The object of the class AdditionWindowViewModel, for the binding.</param>
        /// <param name="isBodyFatPercentageShowed">Whether the body fat percentage showed or not.</param>
        public AdditionWindow( ViewModels.AdditionWindowViewModel viewModel, bool isBodyFatPercentageShowed )
        {
            InitializeComponent();

            if( !isBodyFatPercentageShowed )
            {
                this.bodyFatPercentageTextBox.Visibility = Visibility.Hidden;
                this.label6.Visibility = Visibility.Hidden;
                this.label7.Visibility = Visibility.Hidden;
            }

            this.Loaded += (s, e) => {
                this.MinHeight = this.Height;
                this.MinWidth  = this.Width;
            };

            this.viewModel = viewModel;

            this.DataContext = this.viewModel;
        }

        /// <summary>
        /// The event when TextBoxes is input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPrice_PreviewTextInput( object sender, TextCompositionEventArgs e )
        {
            // 0-9のみ
            e.Handled = !new System.Text.RegularExpressions.Regex( "[0-9.]" ).IsMatch( e.Text );
        }

        /// <summary>
        /// The event when TextBoxes is input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPrice_PreviewExecuted( object sender, ExecutedRoutedEventArgs e )
        {
          // 貼り付けを許可しない
            if ( e.Command == ApplicationCommands.Paste )
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// The event when registerButton clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;
        }

        /// <value>The View-Model.</value>
        private ViewModels.AdditionWindowViewModel viewModel;
    }
}
