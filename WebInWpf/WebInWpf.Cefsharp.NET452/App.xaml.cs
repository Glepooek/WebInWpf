﻿using CefSharp.Wpf;
using CefSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;

// https://gitcode.com/open-source-toolkit/cefa1/overview
// cef 109.1.18

// https://github.com/cefsharp/CefSharp/issues/1714
// https://www.cnblogs.com/xietianjiao/p/11599117.html#:~:text=%E7%94%A8%20anycpu%20%E7%BC%96%E8%AF%91%E7%9A%84%E5%8F%AF%E6%89%A7%E8%A1%8C%E6%96%87%E4%BB%B6%E5%B0%86%E5%9C%A8%2064%20%E4%BD%8D%20CLR%20%E4%B8%8A%E6%89%A7%E8%A1%8C%E3%80%82%20%E7%94%A8,%E7%94%A8%20x64%20%E7%BC%96%E8%AF%91%E7%9A%84%E7%A8%8B%E5%BA%8F%E9%9B%86%E6%97%A0%E6%B3%95%E8%BF%90%E8%A1%8C%E3%80%82%20%E8%BF%99%E6%98%AF%E5%9C%A8%E5%8F%B3%E5%87%BB%E5%B1%9E%E6%80%A7%EF%BC%8C%E9%80%89%E6%8B%A9%E9%A6%96%E9%80%8932%E4%BD%8D%E6%89%8D%E4%BC%9A%E4%BD%BF%E7%94%A8%E7%9A%84%E6%96%B9%E6%B3%95%EF%BC%8C%E5%BF%85%E9%A1%BB%E4%BD%BF%E7%94%A8%20.net%20framework%204.5%20%E4%BB%A5%E4%B8%8A%E6%89%8D%E5%8F%AF%E4%BB%A5%E4%BD%BF%E7%94%A8%E3%80%82

namespace WebInWpf.Cefsharp.NET452
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CefRuntime.SubscribeAnyCpuAssemblyResolver();
            InitializeCef();
        }

        private void InitializeCef()
        {
            CefSharpSettings.FocusedNodeChangedEnabled = true;
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            CefSharpSettings.WcfEnabled = true;
            CefSharpSettings.ShutdownOnExit = true;

            var settings = new CefSettings()
            {
                // 启用日志打印
                LogSeverity = LogSeverity.Warning,
                LogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"CefSharp\\{DateTime.Now:yyyy-MM-dd}.log"),
                UserDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\UserData"),
                // By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                // Set BrowserSubProcessPath based on app bitness at runtime
                BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe"),
            };

            // setting a command line argument
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("disable-application-cache", "1");
            settings.CefCommandLineArgs.Add("allow-file-access-from-files", "1");
            settings.CefCommandLineArgs.Add("disable-gpu", "1"); // 禁用gpu,解决闪烁的问题
            settings.CefCommandLineArgs.Add("touch-events", "1");

            if (!Cef.IsInitialized)
            {
                //Cef.EnableHighDPISupport();
                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            }
        }
    }
}
