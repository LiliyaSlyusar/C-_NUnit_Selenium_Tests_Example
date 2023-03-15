using OpenQA.Selenium;
using LoggerService;
using JetBrains.Annotations;
using Core.Helpers;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium.Support.UI;

namespace Core.Driver
{
    public class WaitUntilExtensions
    {
        private static TimeSpan DefaultWait => TimeSpan.FromSeconds(ConfigurationHelper.Get<double>("DefaultWait"));

        #region public WaitUntil methods
        public static void DocumentIsLoaded()
        {
            LoggerManager.LogInfo("Wait for document loading is finished");
            ConditionIsMet(d => ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState")
                .ToString()
                .Equals("complete"));
        }
        /// <summary>
        /// The Driver waits for the specified condition to become true.
        /// </summary>
        /// <param name="predicate">Enter a function that acts on an IWebElement object.</param> 
        /// <returns>An IWebElement object that the Driver was looking for, based on the wait condition.</returns> 
        public static IWebElement ConditionIsMet(Func<IWebDriver, IWebElement> predicate)
        {
            LoggerManager.LogInfo($"Waiting until condition on page {predicate.Target.ToString().Split('+')[0]} is met");
            return WaitUntilConditionIsMet(predicate, DefaultWait);
        }

        /// <summary>
        /// The Driver waits for the specified condition to become true.
        /// </summary>
        /// <param name="predicate">Enter a function that acts on an IWebElement object.</param> 
        /// <param name="duration">Designate how long to wait for the condition to become true before throwing an error.</param> 
        /// <returns>An IWebElement object that the Driver was looking for, based on the wait condition.</returns> 
        public static IWebElement ConditionIsMet(Func<IWebDriver, IWebElement> predicate, TimeSpan duration)
        {
            LoggerManager.LogInfo($"Waiting until condition on page {predicate.Target.ToString().Split('+')[0]} is met");
            return WaitUntilConditionIsMet(predicate, duration);
        }

        /// <summary>
        /// The Driver waits for the specified condition to become true.
        /// </summary>
        /// <param name="predicate">Enter a function that acts on an IWebElement object. Use OpenQA.Selenium.Support.UI.ExpectedConditions.</param>
        /// <returns>True/False based on the result of the condition.</returns>
        public static bool ConditionIsMet(Func<IWebDriver, bool> predicate)
        {
            //GUITestLogger.LogInfo(String.Format("Waiting until condition on page {0} is met", predicate.Target.ToString().Split('+')[0]));
            return WaitUntilConditionIsMet(predicate, DefaultWait);
        }

        /// <summary>
        /// The Driver waits for the specified condition to become true.
        /// </summary>
        /// <param name="predicate">Enter a function that acts on an IWebElement object. Use OpenQA.Selenium.Support.UI.ExpectedConditions.</param>
        /// <param name="duration">Designate how long to wait for the condition to become true before throwing an error.</param>
        /// <returns>True/False based on the result of the condition.</returns>
        public static bool ConditionIsMet(Func<IWebDriver, bool> predicate, TimeSpan duration)
        {
            return WaitUntilConditionIsMet(predicate, duration);
        }

        /// <summary>
        /// The Driver waits for a web element to exist.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <returns>The IWebElement found by the Driver, or null if not found.</returns>
        [JetBrains.Annotations.NotNull]
        public static IWebElement Exists(By locator)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} exists");
            return WaitUntilExistsOrVisible((d) => {
                var elem = locator.FindElement(d);
                if (elem.Enabled && elem.Displayed)
                    return elem;
                return null;
            }, DefaultWait, false);
        }

        /// <summary>
        /// The Driver waits for a web element to exist.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <param name="seconds">Designate how long to wait for the condition to become true before throwing an error.</param>
        /// <param name="isSuppressingErrors">Set whether to ignore the error thrown if the wait condition fails.</param>
        /// <returns>The IWebElement found by the Driver, or null if not found.</returns>
        [CanBeNull]
        public static IWebElement Exists(By locator, TimeSpan seconds, bool isSuppressingErrors)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} exists");
            return WaitUntilExistsOrVisible((d) => {
                var elem = locator.FindElement(d);
                if (elem.Enabled && elem.Displayed)
                    return elem;
                return null;
            }, seconds, isSuppressingErrors);
        }

        /// <summary>
        /// The Driver waits for an existing web element to not exist or become not visible.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <returns>True if the element is no longer visible, or false if it is visible.</returns>
        public static bool DoesNotExist(By locator)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} does not exists");
            return WaitUntilDoesNotExistOrNotVisible((d) => {
                var elem = locator.FindElement(d);
                return !elem.Enabled && !elem.Displayed;

            }, DefaultWait, false);
        }

        /// <summary>
        /// The Driver waits for an existing web element to not exist or become not visible.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <param name="seconds">Designate how long to wait for the condition to become true before throwing an error.</param>
        /// <param name="isSuppressingErrors">Set whether to ignore the error thrown if the wait condition fails.</param>
        /// <returns>True if the element is no longer visible, or false if it is visible.</returns>
        public static bool DoesNotExist(By locator, TimeSpan seconds, bool isSuppressingErrors)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} does not exists");
            return WaitUntilDoesNotExistOrNotVisible((d) => {
                var elem = locator.FindElement(d);
                return !elem.Enabled && !elem.Displayed;

            }, seconds, isSuppressingErrors);
        }

        /// <summary>
        /// The Driver waits for a web element to become visible.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <returns>The IWebElement found by the Driver, or null if not found.</returns>
        public static IWebElement IsVisible(By locator)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} is visible");
            return WaitUntilExistsOrVisible((d) => {
                var elem = locator.FindElement(d);
                if (elem.Enabled && elem.Displayed)
                    return elem;
                return null;
            }, DefaultWait, false);
        }

        /// <summary>
        /// The Driver waits for a web element to become visible.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <param name="seconds">Designate how long to wait for the condition to become true before throwing an error.</param>
        /// <param name="isSuppressingErrors">Set whether to ignore the error thrown if the wait condition fails.</param>
        /// <returns>The IWebElement found by the Driver, or null if not found.</returns>
        [CanBeNull]
        public static IWebElement IsVisible(By locator, TimeSpan seconds, bool isSuppressingErrors)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} is visible");
            return WaitUntilExistsOrVisible((d) => {
                var elem = locator.FindElement(d);
                if (elem.Enabled && elem.Displayed)
                    return elem;
                return null;
                }, seconds, isSuppressingErrors);
        }

        /// <summary>
        /// The Driver waits for an existing web element to not be visible.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <returns>True if the element is no longer visible, or false if it is visible.</returns>
        public static bool IsNotVisible(By locator)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} is not visible");
            return WaitUntilDoesNotExistOrNotVisible((d) => {
                var elem = locator.FindElement(d);
                return !elem.Enabled && !elem.Displayed;
                    
            }, DefaultWait, false);
        }

        /// <summary>
        /// The Driver waits for an existing web element to not be visible.
        /// </summary>
        /// <param name="locator">The method the Driver will use to locate the IWebElement.</param>
        /// <param name="seconds">Designate how long to wait for the condition to become true before throwing an error.</param>
        /// <param name="isSuppressingErrors">Set whether to ignore the error thrown if the wait condition fails.</param>
        /// <returns></returns>
        public static bool IsNotVisible(By locator, TimeSpan seconds, bool isSuppressingErrors)
        {
            LoggerManager.LogInfo($"Waiting until element at {locator} is not visible");
            return WaitUntilDoesNotExistOrNotVisible((d) => {
                var elem = locator.FindElement(d);
                return !elem.Enabled && !elem.Displayed;

            }, seconds, isSuppressingErrors);
        }

        public static void MaskNotVisible()
        {
            using (new TempChangeSearchTimeout(TimeSpan.Zero))
            {
                DoesNotExist(By.CssSelector(".x-page-mask.x-masked"));
            }
        }

        public static void MaskNotVisible(TimeSpan delay)
        {
            using (new TempChangeSearchTimeout(delay))
            {
                DoesNotExist(By.CssSelector(".x-page-mask.x-masked"));
            }
        }

        #endregion

        #region Main Wait Until Logic

        // TODO: Refactor, don't return null
        private static IWebElement WaitUntilExistsOrVisible(Func<IWebDriver, IWebElement> condition, TimeSpan duration, bool isSuppressingErrors)
        {
            IWebElement element;

            if (isSuppressingErrors)
            {
                try
                {
                    element = WaitUntilConditionIsMet(condition, duration);
                }
                catch (WebDriverTimeoutException)
                {
                    LoggerManager.LogInfo("Timeout exceeded before wait until condition was met.");
                    return null;
                }
            }
            else
            {
                element = WaitUntilConditionIsMet(condition, duration);
            }

            return element;
        }

        private static IWebElement WaitUntilConditionIsMet(Func<IWebDriver, IWebElement> condition, TimeSpan duration)
        {
            return new WebDriverWait(WebDriver.GetDriver(), duration).Until(condition);
        }

        private static bool WaitUntilDoesNotExistOrNotVisible(Func<IWebDriver, bool> condition, TimeSpan duration, bool isSuppressingErrors)
        {
            bool result = false;

            if (isSuppressingErrors)
            {
                try
                {
                    result = WaitUntilConditionIsMet(condition, duration);
                }
                catch (WebDriverException)
                {
                    LoggerManager.LogInfo("Timeout exceeded before wait until condition was met.");
                }
            }
            else
            {
                result = WaitUntilConditionIsMet(condition, duration);
            }

            return result;
        }

        private static bool WaitUntilConditionIsMet(Func<IWebDriver, bool> condition, TimeSpan duration)
        {
            return new WebDriverWait(WebDriver.GetDriver(), duration).Until(condition);
        }

        #endregion
    }
}
