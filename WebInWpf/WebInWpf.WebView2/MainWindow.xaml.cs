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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebInWpf.WebView2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += async (sender, args) =>
            {
                await this.webview.EnsureCoreWebView2Async();
            };
        }

        Microsoft.Web.WebView2.Wpf.WebView2 webView2;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Owner = this;

            webView2 = new Microsoft.Web.WebView2.Wpf.WebView2()
            {
                Source = new Uri("https://www.baidu.com/s?wd=6%E5%A4%A9%E8%B6%85200%E4%BE%8B%E9%98%B3%E6%80%A7+%E5%AE%98%E6%96%B9%E6%B4%BE%E7%BB%84%E8%B5%B4%E7%9A%96%E5%8C%97%E5%B0%8F%E5%9F%8E&sa=fyb_n_homepage&rsv_dl=fyb_n_homepage&from=super&cl=3&tn=baidutop10&fr=top1000&rsv_idx=2&hisfilter=1")
            };
            webView2.CoreWebView2InitializationCompleted += WebView2_CoreWebView2InitializationCompleted;

            window.Content = webView2;
            
            window.Loaded += async (sender, args) =>
            {
                await webView2.EnsureCoreWebView2Async();
            };
            window.Closed += (sender, args) =>
            {
                window.Owner.Show();
            };
            window.Show();
            window.Owner.Hide();
        }

        private async void WebView2_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                webView2.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                webView2.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
            }
            
        }

        private async void CoreWebView2_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            await webView2.CoreWebView2.ExecuteScriptAsync(@"
                document.getElementsByTagName('body')[0].style.overflow='hidden';");
        }

        private async void CoreWebView2_NewWindowRequested(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.NewWindow = webView2.CoreWebView2;
        }
    }
}
