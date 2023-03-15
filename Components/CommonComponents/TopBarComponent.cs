using Components.Pages;
using HtmlElements.Elements;
using HtmlElements.Extensions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Components.CommonComponents
{

    public class TopBarComponent : HtmlElement
    {
        public TopBarComponent(IWebElement webElement) : base(webElement)
        {
        }

        #region Elements
        [FindsBy(Using = "srch-term")]
        private HtmlInput GlobalSearch { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[href = '/contact']")]
        private HtmlLink ContactLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[href = '/help']")]
        private HtmlLink HelpLink { get; set; }

        #endregion

        #region Actions

        
        public ContactUs ClickContactLink() => PageNavigation.NavigateTo<ContactUs>(ContactLink.WaitForVisible().Click);

        public ContactUs ClickHelpLink()
        {
            return PageNavigation.NavigateTo<ContactUs>(ContactLink.WaitForVisible().Click); 
        }

        public void Search(string value)
        {
            GlobalSearch.Click();
            GlobalSearch.EnterText(value);
            GlobalSearch.SendKeys(Keys.Enter);
        }
        #endregion
    }
}