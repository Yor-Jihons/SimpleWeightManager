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
                this.ReflectGraph();
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
        /// The event when the item1 of the menu. (To add weights.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Items1_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            var additionWindowViewModel = new ViewModels.AdditionWindowViewModel();
            var additionWindow = new AdditionWindow( additionWindowViewModel );

            var newWeight = this.dateWeightManager.FetchLatestWeight();
            if( newWeight != null )
            {
                additionWindowViewModel.Height     = newWeight.Height;
                additionWindowViewModel.Weight2Aim = newWeight.Weight2Aim;
            }
            additionWindowViewModel.Notes = notesTextBox.Text;
            additionWindowViewModel.BodyFatPercentage = "0";
            if( additionWindow.ShowDialog() == true )
            {
                item2.IsEnabled = true;

                var newest = ClassMappings.DateWeight.Create(
                    additionWindowViewModel.TargetDate,
                    additionWindowViewModel.Height,
                    additionWindowViewModel.Weight,
                    additionWindowViewModel.BodyFatPercentage,
                    additionWindowViewModel.Weight2Aim
                );

                if( dateWeightManager.Has( newest ) )
                {
                    System.Windows.MessageBox.Show( "すでにあるため記録しませんでした。" );
                    return;
                }

                dateWeightManager.Add( newest );
                this.dateWeightManager.Save();
                this.ReflectGraph();
                this.ReflectDataCards();
                this.ReflectMessage();
                notesTextBox.Text = additionWindowViewModel.Notes;
            }
        }

        /// <summary>
        /// The event when the item2 of the menu. (To delete all weights.)
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

            string defaultStr = "";

            latestDateTextBox.Text = defaultStr;
            latestHeightTextBox.Text = defaultStr;
            latestWeightTextBox.Text = defaultStr;
            latestBMITextBox.Text = defaultStr;
            latestBestWeightTextBox.Text = defaultStr;
            lastestBodyFatPercentageTextBox.Text = defaultStr;

            prevDateTextBox.Text = defaultStr;
            prevHeightTextBox.Text = defaultStr;
            prevWeightTextBox.Text = defaultStr;
            prevBMITextBox.Text = defaultStr;
            prevBestWeightTextBox.Text = defaultStr;
            prevBodyFatPercentageTextBox.Text = defaultStr;

            aimTextBox.Text = defaultStr;
            mesTextBox.Text = defaultStr;
            notesTextBox.Text = defaultStr;

            item2.IsEnabled = false;
        }

        /// <summary>
        /// To reflect the data to the graph.
        /// </summary>
        private void ReflectGraph()
        {
            wpfPlot1.Plot.XTicks( new string[]{} );
            wpfPlot1.Plot.Clear();

            // [Official demo](https://github.com/ScottPlot/ScottPlot/blob/1f68020e0c874140cd668752cf9769398727c3a8/src/demo/ScottPlot.Demo.WPF/WpfDemos/PlotInScrollViewer.xaml.cs)
            wpfPlot1.Configuration.ScrollWheelZoom = false;

            if( !dateWeightManager.IsEmpty() )
            {
                var graphElement = dateWeightManager.Fetch();

                wpfPlot1.Plot.XTicks( graphElement.Xticks );
                wpfPlot1.Plot.Title( "体重の変動" );
                wpfPlot1.Plot.AddSignalXY( graphElement.Ticks, graphElement.Weights, label: "体重" );
                wpfPlot1.Plot.AddSignalXY( graphElement.Ticks, graphElement.BodyFatPercentages, label: "体脂肪率" );
                wpfPlot1.Plot.XAxis.TickLabelStyle( rotation: 45 );
                wpfPlot1.Plot.Render();
            }
            wpfPlot1.Refresh();
        }

        /// <summary>
        /// To reflect the the message to some textboxes.
        /// </summary>
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

            string defaultStr = "---";

            latestDateTextBox.Text               = defaultStr;
            latestHeightTextBox.Text             = defaultStr;
            latestWeightTextBox.Text             = defaultStr;
            latestBMITextBox.Text                = defaultStr;
            latestBestWeightTextBox.Text         = defaultStr;
            lastestBodyFatPercentageTextBox.Text = defaultStr;

            prevDateTextBox.Text              = defaultStr;
            prevHeightTextBox.Text            = defaultStr;
            prevWeightTextBox.Text            = defaultStr;
            prevBMITextBox.Text               = defaultStr;
            prevBestWeightTextBox.Text        = defaultStr;
            prevBodyFatPercentageTextBox.Text = defaultStr;

            var newWeight = this.dateWeightManager.FetchLatestWeight();
            if( newWeight != null )
            {
                latestDateTextBox.Text               = newWeight.ToDateString( ClassMappings.DateWeightDateType.ForDataCard, defaultStr );
                latestHeightTextBox.Text             = newWeight.ToHeightString( defaultStr );
                latestWeightTextBox.Text             = newWeight.ToWeightString( defaultStr );
                latestBMITextBox.Text                = ClassMappings.DateWeight.CalcBMI( newWeight, defaultStr );
                latestBestWeightTextBox.Text         = ClassMappings.DateWeight.CalcBestWeight( newWeight, defaultStr );
                lastestBodyFatPercentageTextBox.Text = newWeight.ToBodyFatPercentageString( defaultStr );
            }

            var prevWeight = this.dateWeightManager.FetchPrevWeight();
            if( prevWeight != null )
            {
                prevDateTextBox.Text              = prevWeight.ToDateString( ClassMappings.DateWeightDateType.ForDataCard, defaultStr );
                prevHeightTextBox.Text            = prevWeight.ToHeightString( defaultStr );
                prevWeightTextBox.Text            = prevWeight.ToWeightString( defaultStr );
                prevBMITextBox.Text               = ClassMappings.DateWeight.CalcBMI( prevWeight, defaultStr );
                prevBestWeightTextBox.Text        = ClassMappings.DateWeight.CalcBestWeight( prevWeight, defaultStr );
                prevBodyFatPercentageTextBox.Text = prevWeight.ToBodyFatPercentageString( defaultStr );
            }
        }

        private ClassMappings.DateWeightManager dateWeightManager;
    }
}
