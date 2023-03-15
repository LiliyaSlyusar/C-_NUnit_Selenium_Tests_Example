using Core.Contracts;
using Core.Driver;
using HtmlElements.Elements;
using LoggerService;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebElement
{
    public static class WebElementExtensions
    {
        /// <summary>
        /// Scroll to element until visible
        /// </summary>
        /// <param name="element"></param>
        public static HtmlElement ScrollToElement(this HtmlElement element)
        {
            
            LoggerManager.LogInfo($"Scrolling to '{element.GetAttribute("id")}' performed");
            var jsDriver = (IJavaScriptExecutor)Driver.WebDriver.GetDriver();
            jsDriver.ExecuteScript("arguments[0].scrollIntoView(true);", element.WrappedElement);
            return element;
        }
    }
}
