using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser
{
    public class BrowserClient
    {
        List<string> lastSites = new List<string>();
        private string host = "";
        private string url = "/";
        private string currentSiteData = "";
        public void Start(string server, float httpVersion = 0.9f, int port = 80)
        {
            LoadSite(server, httpVersion, port);
            host = server;
            lastSites.Add(server);
            while (true)
            {
                var hyperLinks = GetHyperLinks(currentSiteData);
                for (int i = 0; i < hyperLinks.Length; i++)
                {
                    Console.WriteLine($"Index {i}: {hyperLinks[i]}");
                }
                Console.WriteLine("Pick Destination");

                while (true)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int inputInt))
                    {
                        if (inputInt < hyperLinks.Length - 1 && inputInt >= 0)
                        {
                            LoadSite(lastSites[^1] + "/" + hyperLinks[inputInt]);
                            break;
                        }
                        Console.WriteLine("Index value too high/low");
                    }
                    else
                    {
                        Console.WriteLine("Not a number");
                    }
                }

                //break;
            }
        }

        void LoadSite(string server, float httpVersion = 0.9f, int port = 80)
        {
            string httpVersionString = httpVersion.ToString();
            if (httpVersion % 1 == 0) httpVersionString += ".0";
            Console.WriteLine("Client running: HTTP/" + httpVersionString);
            try
            {
                var client = new TcpClient(server, port);
                //var data = System.Text.Encoding.ASCII.GetBytes($"GET /{httpVersionString}\r\n");

                var data = System.Text.Encoding.ASCII.GetBytes($"GET / HTTP/{httpVersionString}\r\nHost: {server}\r\n\r\n");
                NetworkStream stream = client.GetStream();

                var streamWriter = new StreamWriter(stream, Encoding.ASCII);

                streamWriter.Write($"GET / HTTP/{httpVersionString}\r\nHost: {server}\r\n\r\n");
                streamWriter.Flush();
                //stream.Write(data, 0, data.Length);
                
                // Buffer to store the response bytes.
                //data = new Byte[4096*20];

                var streamReader = new StreamReader(stream);
                
                currentSiteData = streamReader.ReadToEnd();

                Console.WriteLine(GetTitle(currentSiteData));

                //Console.WriteLine(GetTitle(result));
                //Console.WriteLine("Received: {0}", result);
                
                lastSites.Add(currentSiteData);
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

        string[] GetHyperLinks(string targetedSite)
        {
            var startSignature = "href=";
            var endSignature = ">";
            var results = new List<string>();
            //Console.WriteLine("BeforeTrim:" + targetedSite);

            while (targetedSite.Contains(startSignature))
            {
                targetedSite = targetedSite.Substring(targetedSite.IndexOf(startSignature) + startSignature.Length+1);
                var linkEndPoint = targetedSite.IndexOf(endSignature);
                if(targetedSite[0] != "/".ToCharArray()[0]) results.Add(targetedSite.Substring(0, linkEndPoint-1));
                
                targetedSite = targetedSite.Substring(linkEndPoint);
            }

            return results.ToArray();
        }

        string GetTitle(string targetedSite)
        {
            var titleTag = "<title>";
            var titleTagIndex = targetedSite.IndexOf(titleTag);
            string title = string.Empty;
            if (titleTagIndex != -1)
            {
                var titleEndTag = targetedSite.IndexOf("</title>");
                if (titleEndTag > titleTagIndex)
                {
                    title = targetedSite.Substring(titleTagIndex +titleTag.Length, titleEndTag-titleTagIndex -titleTag.Length);
                }
                else
                {
                    Console.WriteLine("Broke");
                }
                
            }
            else
            {
                Console.WriteLine("Broke");
            }
            return title;
        }

        private void ReturnToPrevious()
        {
            
        }
    }
}