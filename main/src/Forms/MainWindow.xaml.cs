using System.Reflection;
using System.IO;
using System.Xml.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            item2.IsEnabled = false;

            var thisDirpath = Directory.GetParent( Environment.GetCommandLineArgs()[0] ).ToString();
            var xmlFilepath = System.IO.Path.Join( thisDirpath, "weights.xml" );

            dateWeightManager = ClassMappings.DateWeightManager.Create( xmlFilepath );
            if( dateWeightManager == null )
            {
                dateWeightManager = ClassMappings.DateWeightManager.CreateAsNew( xmlFilepath );
                dateWeightManager.Save();
                return;
            }
            item2.IsEnabled = true;
            this.ReflectGraph();
            this.ReflectDataCards();
            this.ReflectMessage();
        }

        /// <summary>
        /// The event when this window is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing( object sender, System.ComponentModel.CancelEventArgs e )
        {
            //this.dateWeightManager.Save();
        }

        /// <summary>
        /// The event when the item1 of the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Items1_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            var additionWindowViewModel = new ViewModels.AdditionWindowViewModel();
            var additionWindow = new AdditionWindow( additionWindowViewModel );

            ClassMappings.DateWeight newWeight, prevWeight;
            dateWeightManager.FetchWeights( out newWeight, out prevWeight );
            if( newWeight != null )
            {
                additionWindowViewModel.Height     = newWeight.Height;
                additionWindowViewModel.Weight2Aim = newWeight.Weight2Aim;
            }
            additionWindowViewModel.Notes = notesTextBox.Text;
            if( additionWindow.ShowDialog() == true )
            {
                item2.IsEnabled = true;
                dateWeightManager.Add(
                    ClassMappings.DateWeight.Create(
                        additionWindowViewModel.TargetDate,
                        additionWindowViewModel.Height,
                        additionWindowViewModel.Weight,
                        additionWindowViewModel.Weight2Aim
                    )
                );
                this.dateWeightManager.Save();
                this.ReflectGraph();
                this.ReflectDataCards();
                this.ReflectMessage();
                notesTextBox.Text = additionWindowViewModel.Notes;
            }
        }

        /// <summary>
        /// The event when the item2 of the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Items2_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            var res = MessageBox.Show( "本当に初期化しますか?", "注意", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No, MessageBoxOptions.None );
            if( res != MessageBoxResult.Yes ) return;

            this.dateWeightManager.Clear();
            this.dateWeightManager.Save();

            this.ReflectGraph();

            latestDateTextBox.Text = "---";
            latestHeightTextBox.Text = "---";
            latestWeightTextBox.Text = "---";
            latestBMITextBox.Text = "---";
            latestBestWeightTextBox.Text = "---";

            prevDateTextBox.Text = "---";
            prevHeightTextBox.Text = "---";
            prevWeightTextBox.Text = "---";
            prevBMITextBox.Text = "---";
            prevBestWeightTextBox.Text = "---";

            aimTextBox.Text = "---";
            mesTextBox.Text = "---";
            notesTextBox.Text = "---";

            item2.IsEnabled = false;
        }

        private void ReflectGraph()
        {
            wpfPlot1.Plot.XTicks( new string[]{} );
            wpfPlot1.Plot.Clear();

            if( !dateWeightManager.IsEmpty() )
            {
                int N = dateWeightManager.Count();
                double[] plotX  = new double[N];
                double[] plotY  = new double[N];
                string[] xticks = new string[N];
                dateWeightManager.Fetch( plotX, plotY, xticks );

                wpfPlot1.Plot.XTicks( xticks );
                wpfPlot1.Plot.Title( "体重の変動" );
                wpfPlot1.Plot.AddSignalXY( plotX, plotY );
                wpfPlot1.Plot.XAxis.TickLabelStyle( rotation: 45 );
                wpfPlot1.Plot.Render();
            }
            wpfPlot1.Refresh();
        }

        public void ReflectMessage()
        {
            aimTextBox.Text = dateWeightManager.ToAimString();
            mesTextBox.Text = dateWeightManager.ToMessageString();
        }


        public void ReflectDataCards()
        {
            if( dateWeightManager.Count() == 0 )
            {
                return;
            }
            latestDateTextBox.Text       = "---";
            latestHeightTextBox.Text     = "0.0cm";
            latestWeightTextBox.Text     = "0.0kg";
            latestBMITextBox.Text        = "---";
            latestBestWeightTextBox.Text = "0.0kg";

            prevDateTextBox.Text       = "---";
            prevHeightTextBox.Text     = "0.0cm";
            prevWeightTextBox.Text     = "0.0kg";
            prevBMITextBox.Text        = "---";
            prevBestWeightTextBox.Text = "0.0kg";

            ClassMappings.DateWeight newWeight = null, prevWeight = null;
            this.dateWeightManager.FetchWeights( out newWeight, out prevWeight );
            if( newWeight != null )
            {
                latestDateTextBox.Text       = newWeight.ToDateString( ClassMappings.DateWeightDateType.ForDataCard );
                latestHeightTextBox.Text     = newWeight.ToHeightString();
                latestWeightTextBox.Text     = newWeight.ToWeightString();
                latestBMITextBox.Text        = ClassMappings.DateWeight.CalcBMI( newWeight );
                latestBestWeightTextBox.Text = ClassMappings.DateWeight.CalcBestWeight( newWeight );
            }

            if( prevWeight != null )
            {
                prevDateTextBox.Text       = prevWeight.ToDateString( ClassMappings.DateWeightDateType.ForDataCard );
                prevHeightTextBox.Text     = prevWeight.ToHeightString();
                prevWeightTextBox.Text     = prevWeight.ToWeightString();
                prevBMITextBox.Text        = ClassMappings.DateWeight.CalcBMI( prevWeight );
                prevBestWeightTextBox.Text = ClassMappings.DateWeight.CalcBestWeight( prevWeight );
            }
        }

        private ClassMappings.DateWeightManager dateWeightManager;
    }
}
