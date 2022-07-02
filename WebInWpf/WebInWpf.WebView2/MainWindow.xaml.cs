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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Owner = this;
            window.Owner.Hide();
            Microsoft.Web.WebView2.Wpf.WebView2 webView2 = new Microsoft.Web.WebView2.Wpf.WebView2()
            {
                Source = new Uri("https://www.bing.com")
            };
            window.Content = webView2;
            window.Show();
            window.Closed += (sender, args) =>
            {
                window.Owner.Show();
            };
        }
    }
}
