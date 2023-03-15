using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Driver
{
    /// <summary>
    /// Provides methods for managing the WebDriver instance used by the test framework.
    /// </summary>
    public static class WebDriver
    {
        private static IWebDriver _driver = null;
        private static readonly object Padlock = new object();

        /// <summary>
        /// Gets the current WebDriver instance, creating one if necessary.
        /// </summary>
        /// <returns>The current WebDriver instance.</returns>
        public static IWebDriver GetDriver()
        {
            if (_driver == null)
                lock (Padlock)
                    if (_driver == null)
                    {
                        var eventFiringWebDriver = new EventFiringWebDriver(DriverFactory.Create());
                        _driver = eventFiringWebDriver;

                    }

            return _driver;
        }
        /// <summary>
        /// Resets the current WebDriver instance, closing the browser window if necessary.
        /// </summary>
        public static void Reset()
        {
            if (_driver != null)
                lock (Padlock)
                    if (_driver != null)
                    {
                        _driver.Quit();
                        _driver = null;
                    }
        }
    }
}
