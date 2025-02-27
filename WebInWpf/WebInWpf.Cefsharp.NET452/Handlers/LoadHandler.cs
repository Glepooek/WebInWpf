using CefSharp;
using System;

namespace WebInWpf.Cefsharp.NET452.Handlers
{
    public class LoadHandler : ILoadHandler
    {
        private Action LoadStart = null;
        private Action<bool> LoadEnd = null;
        public LoadHandler(Action loadStart, Action<bool> loadEnd)
        {
            LoadStart= loadStart;
            LoadEnd = loadEnd;
        }

        public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
            if (LoadEnd != null)
            {
                if (frameLoadEndArgs.Url.Equals("about:blank"))
                {
                    LoadEnd.Invoke(false);
                    return;
                }

                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  OnFrameLoadEnd  " + frameLoadEndArgs.Url);
                LoadEnd.Invoke(true);
            }
        }

        public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {
            if (!frameLoadStartArgs.Url.Equals("about:blank"))
            {
                LoadStart.Invoke();
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  OnFrameLoadStart  " + frameLoadStartArgs.Url);
            }
        }

        public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
        }

        public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
        {
        }
    }
}
