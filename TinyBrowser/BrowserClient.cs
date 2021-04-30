using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace TinyBrowser
{
    public class BrowserClient
    {
        List<string> lastSites = new List<string>();
        public void Start(string server, float httpVersion = 0.9f, int port = 80)
        {
            string httpVersionString = httpVersion.ToString();
            if (httpVersion % 1 == 0) httpVersionString += ".0";
            Console.WriteLine("Client running: HTTP/" + httpVersionString);
            try
            {
                var client = new TcpClient(server, port);
                var data = System.Text.Encoding.ASCII.GetBytes($"GET /{httpVersionString}\r\n");

                data = System.Text.Encoding.ASCII.GetBytes("GET / HTTP/1.1\r\nHost: acme.com\r\n\r\n");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                
                // Buffer to store the response bytes.
                data = new Byte[4096*20];

                var streamReader = new StreamReader(stream);
                
                var result = streamReader.ReadToEnd();
                
                Console.WriteLine("Received: {0}", result);
                
                lastSites.Add(result);
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("ArgumentNullException: {0}", exception);
            }
            catch (SocketException exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }
        }

        void LoadSite(string site, float httpVersion)
        {
            
        }

        private void ReturnToPrevious()
        {
            
        }
    }
}