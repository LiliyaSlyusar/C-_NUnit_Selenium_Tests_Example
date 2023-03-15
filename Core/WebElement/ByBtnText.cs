using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebElement
{
    /// <summary>
    /// Defines a mechanism for locating elements based on their button text content.
    /// </summary>
    public class ByBtnText: By
    {
        private readonly string _text;
        private const string SelectorTemplete = ".//*[contains(text(),'{0}')]";

        /// <summary>
        /// Initializes a new instance of the <see cref="ByBtnText"/> class with the specified button text.
        /// </summary>
        /// <param name="text">The button text to search for.</param>
        public ByBtnText(string text)
        {
            _text = text;
        }
        /// <inheritdoc/>
        public override IWebElement FindElement(ISearchContext context) => context.FindElement(XPath(string.Format(SelectorTemplete, _text)));
        /// <inheritdoc/>
        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context) =>
            context.FindElements(XPath(string.Format(SelectorTemplete, _text)));
    }
}
