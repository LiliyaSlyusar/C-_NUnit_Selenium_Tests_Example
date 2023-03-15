using Core.Helpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerService;

namespace Core.Driver
{
    public enum DriverToUse
    {
        InternetExplorer,
        Chrome,
    }

    public static class DriverFactory
    {

        public static IWebDriver Create()
        {
            IWebDriver driver;
            var driverToUse = ConfigurationHelper.Get<DriverToUse>("DriverToUse");
            var useGrid = ConfigurationHelper.Get<bool>("UseGrid");
            var pagLoadTimeOut = ConfigurationHelper.Get<double>("PageLoadTimeout");

            if (useGrid)
            {
                driver = CreateGridDriver(driverToUse);
            }
            else
            {
                switch (driverToUse)
                {
                    case DriverToUse.InternetExplorer:
                        InternetExplorerOptions options = new InternetExplorerOptions()
                        {
                            IgnoreZoomLevel = true,
                            //PageLoadStrategy = InternetExplorerPageLoadStrategy.Normal,
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true
                        };
                        driver = new InternetExplorerDriver(PathExtractor.GetPathFromOutputDir(string.Empty), options, TimeSpan.FromMinutes(5));
                        break;
                    case DriverToUse.Chrome:
                        driver = new ChromeDriver();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }


            driver.Manage().Window.Maximize();


            driver.SetDefaultPageLoadTimeout(TimeSpan.FromSeconds(pagLoadTimeOut));

            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");

            return driver;
        }

        public static IWebDriver CreateGridDriver(DriverToUse driverToUse)
        {
            var gridUrl = ConfigurationHelper.Get<string>("GridUrl");
            DriverOptions options;

            switch (driverToUse)
            {
                case DriverToUse.InternetExplorer:
                    options = new InternetExplorerOptions();
                    break;
                default:
                    options = new ChromeOptions();
                    break;
            }

            var remoteDriver = new ExtendedRemoteWebDriver(new Uri(gridUrl), options.ToCapabilities(), TimeSpan.FromSeconds(180));
            var nodeHost = remoteDriver.GetNodeHost();
            LoggerManager.LogInfo("Running tests on host " + nodeHost);
            return remoteDriver;
        }

    }
}
