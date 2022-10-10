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
    /// <summary>
    /// Interaction logic for AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AdditionWindow( ViewModels.AdditionWindowViewModel viewModel )
        {
            //InitializeComponent();

            this.m_viewModel = viewModel;

            this.DataContext = m_viewModel;
        }

        /// <summary>
        /// The event when TextBoxes is input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 0-9のみ
            e.Handled = !new System.Text.RegularExpressions.Regex( "[0-9.]" ).IsMatch( e.Text );
        }

        /// <summary>
        /// The event when TextBoxes is input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPrice_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
          // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void RegisterButton_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;
        }

        private ViewModels.AdditionWindowViewModel m_viewModel;
    }
}
