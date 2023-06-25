/**
* @file
* @brief Interaction logic for App.xaml.
*/

using System;
using System.Windows;
using System.Reflection;
using System.Threading.Tasks;


namespace SimpleWeightManager
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            App.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            this.m_mutex = new Mutexes.MutexEx( App.AssemblyName );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            System.Windows.Controls.ToolTipService.ShowDurationProperty.OverrideMetadata(
                typeof(DependencyObject),
                new FrameworkPropertyMetadata(int.MaxValue));

            // UIスレッドの未処理例外で発生
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            // UIスレッド以外の未処理例外で発生
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            // それでも処理されない例外で発生
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Loggers.Logger.SetLogInfo( "logfile", "error.log" );
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            HandleException( exception );
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception.InnerException as Exception;
            HandleException( exception );
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            HandleException( exception );
        }

        private void HandleException(Exception e)
        {
            logger.Fatal( e?.ToString() );
            MessageBox.Show( $"Error occured. Please tell this error to the author.\n\n{e?.ToString()}" );
            Environment.Exit( 1 );
        }

        private void Application_Startup( object sender, System.Windows.StartupEventArgs e )
        {
            // ミューテックスの所有権を要求
            if( m_mutex.HasAlreadyRun() )
            {
                System.Windows.MessageBox.Show( "すでに起動しています!!!" );
                Mutexes.MutexEx.MoveForeground();
                this.Shutdown( -1 );
            }
        }

        private void Application_Exit( object sender, ExitEventArgs e )
        {
            m_mutex.Destruct();
        }

        public static string AssemblyName{ get; private set; }

        private Mutexes.MutexEx m_mutex;

        public static Loggers.Logger logger = new Loggers.Logger();
    }
}
