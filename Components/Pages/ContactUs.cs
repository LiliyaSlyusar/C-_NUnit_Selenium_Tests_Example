using Core.WebElement;
using HtmlElements.Elements;
using HtmlElements.Extensions;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Pages
{
    public class ContactUs : BasePage
    {
        public override string PagePath => "contact";

        #region Elements

        [FindsBy(Using = "Name")]
        private HtmlInput? NameInput { get; set; }

        [FindsBy(Using = "EmailAddress")]
        private HtmlInput? EmailInput { get; set; }

        [FindsBy(Using = "PhoneNumber")]
        private HtmlInput? PhoneInput { get; set; }

        [FindsBy(Using = "Comments")]
        private HtmlInput? CommentsInput { get; set; }

        [FindsBy(Using = "submitContactUs")]
        private HtmlElement? SubmitBtn { get; set; }

        [FindsBy(How = How.Custom, CustomFinderType = typeof(ByBtnText), Using = "Tutorials")]
        private HtmlLink? TutorialsBtn { get; set; }

        [FindsBy(How = How.Custom, CustomFinderType = typeof(ByBtnText), Using = "FAQ")]
        private HtmlLink? FaqBtn { get; set; }

        [FindsBy(Using = "ResultMessage")]
        private HtmlLabel? ResultMessage { get; set; }
        #endregion

        #region Actions

        public void SendRequest(string name, string email, string phone, string comment)
        {
            NameInput?.EnterText(name);
            EmailInput?.EnterText(email);
            PhoneInput?.EnterText(phone);
            CommentsInput?.EnterText(comment);
            SubmitBtn?.Click();
        }

        public void ClickTutorialsBtn() => TutorialsBtn?.Click();

        public void ClickFaqBtn() => FaqBtn?.Click();


        public bool IsElementsPresent() => NameInput.Displayed && EmailInput.Displayed && SubmitBtn.Displayed;

        public bool IsResultMessagePresent() => ResultMessage.WaitForVisible().Displayed;
        #endregion
    }
}
