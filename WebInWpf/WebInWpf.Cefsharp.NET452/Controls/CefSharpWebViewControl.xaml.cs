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
        //加载动画
        Storyboard LoadingAnimated = null;

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
                    }

                    this.BackCommand = Browser.BackCommand;
                    this.ForwardCommand = Browser.ForwardCommand;
                });
            });
        }

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
    }
}
