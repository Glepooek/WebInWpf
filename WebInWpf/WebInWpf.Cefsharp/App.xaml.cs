using CefSharp.Wpf;
using CefSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Unipus.Student.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            InitializeCef();

            this.Startup += OnStartup;
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
        }

        #endregion

        #region EventHandler

        private void OnStartup(object sender, StartupEventArgs e)
        {

        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {

            }
        }

        private void OnTaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {

        }

        #endregion

        #region Methods

        private void InitializeCef()
        {
            CefSharpSettings.FocusedNodeChangedEnabled = true;
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

            var settings = new CefSettings()
            {
                // 禁止日志打印
                LogSeverity = LogSeverity.Disable,
                // By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };

            // setting a command line argument
            settings.CefCommandLineArgs.Add("enable-media-stream");
            settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing");

            if (!Cef.IsInitialized)
            {
                //Cef.EnableHighDPISupport();
                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            }
        }

        #endregion
    }
}
