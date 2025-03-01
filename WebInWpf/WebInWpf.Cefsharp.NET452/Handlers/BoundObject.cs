using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebInWpf.Cefsharp.NET452.Handlers
{
    public class BoundObject
    {
        public void onMessageReceived(string message)
        {
            Console.WriteLine(message);
        }
    }
}
