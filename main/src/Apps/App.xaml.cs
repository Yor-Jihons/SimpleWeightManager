/**
* @file
* @brief Interaction logic for App.xaml.
*/

using System;
using System.Windows;
using System.Threading.Tasks;

namespace SimpleWeightManager
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The method which runs when this application runs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup( StartupEventArgs e )
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
        }

        /// <summary>
        /// The event when UI thread Exceptions thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            HandleException(exception);
        }

        /// <summary>
        /// The event when exceptions (except UI thread) thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception.InnerException as Exception;
            HandleException(exception);
        }

        /// <summary>
        /// The event when other exceptions thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            HandleException(exception);
        }

        /// <summary>
        /// Show the message for exception.
        /// </summary>
        /// <param name="e"></param>
        private void HandleException(Exception e)
        {
            MessageBox.Show( $"エラーが発生しました\n{e?.ToString()}" );
            Environment.Exit(1);
        }

        /// <returns>The mutex.</returns>
        private Mutexes.MutexEx Mutex{ get; set; } = new Mutexes.MutexEx( "SampleWeightManager" );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationStartup( object sender, System.Windows.StartupEventArgs e )
        {
            // ミューテックスの所有権を要求
            if( this.Mutex.HasAlreadyRun() )
            {
                System.Windows.MessageBox.Show( "すでに起動しています!!!" );
                Mutexes.MutexEx.MoveForeground();
                this.Shutdown( -1 );
            }
        }

        /// <summary>
        /// The event when application exited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Exit( object sender, ExitEventArgs e )
        {
            this.Mutex.Destruct();
        }
    }
}
