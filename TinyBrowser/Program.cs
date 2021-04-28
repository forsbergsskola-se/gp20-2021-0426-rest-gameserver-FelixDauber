using System;

namespace TinyBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new BrowserClient();
            client.Start("acme.com", 0.9f);
        }
    }
}
