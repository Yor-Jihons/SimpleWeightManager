/**
* @file
* @brief The class for Mutex.
*/

using System;

namespace SimpleWeightManager.Mutexes
{
    /// <summary>
    /// The class for Mutex.
    /// </summary>
    class MutexEx
    {
         /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">This process name.</param>
        public MutexEx( string name )
        {
            this.Mutex = new System.Threading.Mutex( false, name );
        }

        /// <summary>
        /// Check whether this process has already run or not.
        /// </summary>
        /// <returns>Returns true if this process has already run, otherwise returns false.</returns>
        public bool HasAlreadyRun()
        {
            if( !this.Mutex.WaitOne( 0, false) )
            {
                this.Mutex.Close();
                this.Mutex = null;
                return true;
            }
        return false;
        }

        /// <summary>
        /// Destruct this process.
        /// </summary>
        /// <returns>Returns true if process is deleted, otherwise returns false.</returns>
        public bool Destruct()
        {
            if( this.Mutex != null )
            {
                this.Mutex.ReleaseMutex();
                this.Mutex.Close();
                this.Mutex = null;
                return true;
            }
        return false;
        }

        /// <summary>
        /// Move the this window to foreground.
        /// </summary>
        /// <returns>Returns true if suceceeded, otherwise returns false.</returns>
        public static bool MoveForeground()
        {
            var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
            var processes   = System.Diagnostics.Process.GetProcessesByName( thisProcess.ProcessName );

            foreach( System.Diagnostics.Process p in processes )
            {
                if( p.Id != thisProcess.Id )
                {
                    ShowWindow( p.MainWindowHandle, SW_NORMAL );
                    SetForegroundWindow( p.MainWindowHandle );
                    return true;
                }
            }
        return false;
        }

        /// <summary>
        /// Call the function "SetForegroundWindow", on Windows API.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow( IntPtr hWnd );

        /// <summary>
        /// Call the function "ShowWindow", on Windows API.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow( IntPtr hWnd, int nCmdShow );

        /// <value>
        /// The contant value for the function "ShowWindow".
        /// </value>
        const int SW_NORMAL = 1;

        /// <value>
        /// The object of the class Mutex.
        /// </value>
        private System.Threading.Mutex Mutex{ get; set; }
    }
}
