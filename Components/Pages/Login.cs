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
    public class Login : BasePage
    {
        public override string PagePath => "Account/Login";
        
        #region Elements
        [FindsBy(Using = "Email")]
        private HtmlInput EmailField { get; set; }

        [FindsBy(Using = "Password")]
        private HtmlInput PasswordField { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'form-group']/div/div[@class = 'rememberMe-checkbox_container']")]
        private HtmlCheckBox RememberMeCheckBox { get; set; }

        [FindsBy(Using = "auth-sso_button")]
        private HtmlElement SingleSignOnBtn { get; set; }

        [FindsBy(Using = "signin-button_submit")]
        private HtmlElement SignInBtn { get; set; }

        [FindsBy(Using = "forgotPassword")]
        private HtmlLink ForgotPasswordLink { get; set; }

        [FindsBy(Using = "Password-error")]
        private HtmlLabel PassIsRequiresLabel { get; set; }

        [FindsBy(Using = "Email-error")]
        private HtmlLabel EmailNotValidLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form div li")]
        private HtmlLabel InvalidLoginErrorLabel { get; set; }


        #endregion

        #region Actions

        public Home LoginToApp(string email, string password)
        {
            EmailField.EnterText(email);
            PasswordField.EnterText(password);
            SignInBtn.WaitUntil(item => item.Enabled, "Button is not enabled");

            return PageNavigation.NavigateTo<Home>(SignInBtn.Click);
        }
        #endregion

        #region SingleMethodActions

        
        public Login SetEmail(string email)
        {
            EmailField.EnterText(email);
            return this;
        }
        
        public Login SetPassword(string pass)
        {
            PasswordField.EnterText(pass);
            return this;
        }
        
        public Login SetRememberMe(bool value)
        {
            if(value)
                RememberMeCheckBox.Click();
            return this;
        }
        
        public void ClickSignIn() => SignInBtn.Click();
        
        public ForgotPassword ClickForgotPasswordLink() => PageNavigation.NavigateTo<ForgotPassword>(ForgotPasswordLink.Click);

        
        public bool IsSignEnabled() => SignInBtn.Enabled;
        
        public string GetErrorEmailText() => EmailNotValidLabel.WaitForVisible().Text;
        
        public string GetInvalidLoginErrorLabelText() => InvalidLoginErrorLabel.WaitForVisible().Text;

        #endregion
    }
}
