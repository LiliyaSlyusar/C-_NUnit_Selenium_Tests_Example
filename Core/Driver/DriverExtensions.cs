using Core.Contracts;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Driver
{
    public static class DriverExtensions
    {
        /// <summary>
        /// Set how long to look for an object on a page when searching
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="seconds"></param>
        public static void SetDefaultObjectSearchTimeout(this IWebDriver driver, TimeSpan seconds)
        {
            var timeouts = driver.Manage().Timeouts();
            timeouts.ImplicitWait = seconds;
        }

        /// <summary>
        /// Set how long to wait for a page to load
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="seconds"></param>
        public static void SetDefaultPageLoadTimeout(this IWebDriver driver, TimeSpan seconds)
        {
            var timeouts = driver.Manage().Timeouts();
            timeouts.PageLoad = seconds;
        }
        public static string SwitchWindow(this IWebDriver driver, ILoggerManager logger)
        {
            //pop up window takes awhile, especially IE. wait for 1 second.
            Thread.Sleep(TimeSpan.FromSeconds(1));

            if (driver.WindowHandles.Count == 1)
            {
                logger.LogInfo("No pop-up window found.");
                return driver.CurrentWindowHandle;
            }

            var currWindowHandle = driver.CurrentWindowHandle;
            var newWindowHandle = driver.WindowHandles.First(w => w != currWindowHandle);
            driver.SwitchTo().Window(newWindowHandle);

            // Suppress the onbeforeunload event first. This prevents the application hanging on a dialog box that does not close.
            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");

            return currWindowHandle;
        }

        public static Screenshot TakeScreenShot()
        {
            return ((ITakesScreenshot)WebDriver.GetDriver()).GetScreenshot();
        }

    }
}
