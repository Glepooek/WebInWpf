using System.Windows;

namespace WebInWpf.Cefsharp.NET452
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewWebView_Click(object sender, RoutedEventArgs e)
        {
            WebViewWindow webViewWindow = new WebViewWindow();
            webViewWindow.Show();
        }
    }
}
