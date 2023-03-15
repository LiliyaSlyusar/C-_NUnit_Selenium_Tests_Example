using Components.PageObjectFactory;
using Core.Driver;
using Core.Helpers;
using HtmlElements;
using LoggerService;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public static class PageNavigation
    {
        public delegate void NavFunction(string parameter);

        public static TPage NavigateTo<TPage>() where TPage : BasePage, new()
        {
            var driver = Core.Driver.WebDriver.GetDriver();
            IPageObjectFactory pageFactory = new HtmlObjectFactory();
            TPage page = new TPage();

            LoggerManager.LogInfo($"Navigation to '{typeof(TPage).Name}'");

            driver.Navigate().GoToUrl(BuildPageUrl(page.PagePath));

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(page.PagePath));

            page = pageFactory.Create<TPage>(driver);
            return page;
        }

        public static TPage NavigateTo<TPage>(string pageUrl) where TPage : BasePage, new()
        {
            LoggerManager.LogInfo($"Navigation to '{typeof(TPage).Name}'");
            var driver = Core.Driver.WebDriver.GetDriver();
            IPageObjectFactory pageFactory = new HtmlObjectFactory();

            driver.Navigate().GoToUrl(pageUrl);

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(pageUrl));

            return pageFactory.Create<TPage>(driver);
        }

        public static TPage NavigateToWithRedirection<TPage>(string pageUrl) where TPage : BasePage, new()
        {
            LoggerManager.LogInfo($"Navigation to '{typeof(TPage).Name}'");
            var driver = Core.Driver.WebDriver.GetDriver();
            IPageObjectFactory pageFactory = new HtmlObjectFactory();
            TPage page = new TPage();

            driver.Navigate().GoToUrl(pageUrl);

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(page.PagePath));

            return pageFactory.Create<TPage>(driver);
        }

        public static TPage NavigateTo<TPage>(Action navFunc) where TPage : BasePage, new()
        {
            IPageObjectFactory pageFactory = new HtmlObjectFactory();

            LoggerManager.LogInfo($"Navigation to '{typeof(TPage).Name}'");

            navFunc();

            TPage page = new TPage();

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(page.PagePath));

            page = pageFactory.Create<TPage>(Core.Driver.WebDriver.GetDriver());

            // PageFactory.InitElements(WebDriver.GetDriver(), page);
            return page;
        }

        public static TPage NavigateTo<TPage>(NavFunction navFunc, string param) where TPage : BasePage, new()
        {
            IPageObjectFactory pageFactory = new HtmlObjectFactory();

            LoggerManager.LogInfo($"Navigation to '{typeof(TPage).Name}'");

            navFunc(param);

            TPage page = new TPage();

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(page.PagePath));

            page = pageFactory.Create<TPage>(Core.Driver.WebDriver.GetDriver());

            return page;
        }

        public static TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            IPageObjectFactory pageFactory = new HtmlObjectFactory();

            LoggerManager.LogInfo($"Getting instance of '{typeof(TPage).Name}' page");

            TPage page = new TPage();

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(page.PagePath));

            page = pageFactory.Create<TPage>(Core.Driver.WebDriver.GetDriver());

            return page;
        }

        public static TPage SwitchControlToWindow<TPage>() where TPage : BasePage, new()
        {
            // pop up window takes awhile, especially IE. wait for 1 second.
            Thread.Sleep(TimeSpan.FromSeconds(1));
            IWebDriver driver = Core.Driver.WebDriver.GetDriver();
            IPageObjectFactory pageFactory = new HtmlObjectFactory();
            TPage page = new TPage();

            var newWindowHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(newWindowHandle);

            driver.Manage().Window.Maximize();

            WaitUntilExtensions.ConditionIsMet((d) => d.Url.Contains(page.PagePath));

            page = pageFactory.Create<TPage>(Core.Driver.WebDriver.GetDriver());

            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");

            return page;
        }

        private static string BuildPageUrl(string pagePath)
        {
            return ConfigurationHelper.Get<string>("BaseUrl") + pagePath;
        }
    }
}

