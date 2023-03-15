using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Driver
{
    /// <summary>
    /// A custom implementation of the RemoteWebDriver class that adds the ability to take screenshots
    /// from a remote browser and retrieve the host information for the node the browser is running on.
    /// </summary>
    public class ExtendedRemoteWebDriver: RemoteWebDriver, ITakesScreenshot
    {
        private readonly Uri _remoteHost;

        /// <summary>
        /// Initializes a new instance of the ExtendedRemoteWebDriver class with the specified remote host,
        /// capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteHost">The URI of the remote host where the browser is running.</param>
        /// <param name="capabilities">The desired capabilities of the browser.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for a command to complete.</param>
        public ExtendedRemoteWebDriver(Uri remoteHost, ICapabilities capabilities, TimeSpan commandTimeout)
            : base(remoteHost, capabilities, commandTimeout)
        {
            _remoteHost = remoteHost;
        }


        /// <summary>
        /// Gets the host information for the node where the remote browser is running.
        /// </summary>
        /// <returns>A string representing the host information for the remote node.</returns>
        public string GetNodeHost()
        {
            var result = "[UNKNOWN HOST]";
            var uri = new Uri($"http://{_remoteHost.Host}:{_remoteHost.Port}/?session={SessionId}");

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            {
                var stream = httpResponse.GetResponseStream();
                if (stream != null)
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        result = JObject.Parse(reader.ReadToEnd()).SelectToken("proxyId").ToString();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Takes a screenshot of the remote browser.
        /// </summary>
        /// <returns>A Screenshot object representing the captured image.</returns>
        public new Screenshot GetScreenshot()
        {
            return new Screenshot(Execute(DriverCommand.Screenshot, null).Value.ToString());
        }
    }
}
