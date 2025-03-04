using CefSharp;
using CefSharp.Wpf.Experimental;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WebInWpf.Cefsharp.NET452.Handlers;

namespace WebInWpf.Cefsharp.NET452.Controls
{
    /// <summary>
    /// Interaction logic for CefSharpWebViewControl.xaml
    /// </summary>
    public partial class CefSharpWebViewControl : UserControl
    {
        #region Fields

        //加载动画
        Storyboard LoadingAnimated = null;

        #endregion

        #region Constructor

        public CefSharpWebViewControl()
        {
            InitializeComponent();

            Browser.LifeSpanHandler = new OpenPageSelf();
            Browser.MenuHandler = new ContextMenuHandler();
            Browser.DownloadHandler = new DownloadHandler();
            Browser.KeyboardHandler = new KeyboardHandler();
            // 处理输入法位置不对的问题
            Browser.WpfKeyboardHandler = new WpfImeKeyboardHandler(Browser);
            Browser.LoadHandler = new LoadHandler(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Console.WriteLine($"web加载开始");

                    if (LoadingAnimated == null)
                    {
                        LoadingAnimated = this.FindResource("LoadingStoryboard") as Storyboard;
                    }
                    LoadingImage.Visibility = Visibility.Visible;
                    LoadingAnimated.Begin(LoadingImage, true);
                });
            },
            e =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Console.WriteLine($"web加载完成");

                    if (e)
                    {
                        LoadingImage.Visibility = Visibility.Collapsed;
                        LoadingAnimated?.Stop(LoadingImage);
                        Browser.JavascriptMessageReceived += Browser_JavascriptMessageReceived;
                    }

                    this.BackCommand = Browser.BackCommand;
                    this.ForwardCommand = Browser.ForwardCommand;
                });
            });

            RegisterBoundObject("webView", new BoundObject());

            //this.Address = "https://www.baidu.com";
            //this.Address = $"{AppDomain.CurrentDomain.BaseDirectory}TestWeb//index.html";
            this.Address = "https://cws-test.unipus.cn/?&id=320431807554125824&version=28&u-app-id=1709&mode=edit";

        }

        #endregion

        #region DependencyProperties

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(CefSharpWebViewControl), new PropertyMetadata(null));

        public ICommand ForwardCommand
        {
            get { return (ICommand)GetValue(ForwardCommandProperty); }
            set { SetValue(ForwardCommandProperty, value); }
        }

        public static readonly DependencyProperty ForwardCommandProperty =
            DependencyProperty.Register("ForwardCommand", typeof(ICommand), typeof(CefSharpWebViewControl), new PropertyMetadata(null));


        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(CefSharpWebViewControl), new PropertyMetadata(null, OnAddressPropertyChanged));

        private static void OnAddressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CefSharpWebViewControl control && e.NewValue != null)
            {
                control.Browser.Load(e.NewValue.ToString());
            }
        }

        #endregion

        #region EventHandles

        private void Browser_JavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 注册回调对象，用于JS调用C#方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objectToBind"></param>
        private void RegisterBoundObject(string name, object objectToBind)
        {
            Browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            Browser.JavascriptObjectRepository.Register(name, objectToBind, true, BindingOptions.DefaultBinder);
        }

        /// <summary>
        /// C#调用JS方法
        /// </summary>
        public void CallJavaScriptAsync(string code, string scriptUrl = "about:blank", int startLine = 1)
        {
            if (!Browser.IsBrowserInitialized)
            {
                return;
            }

            try
            {
                Browser?.GetBrowser()?.MainFrame.ExecuteJavaScriptAsync(code, scriptUrl, startLine);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
