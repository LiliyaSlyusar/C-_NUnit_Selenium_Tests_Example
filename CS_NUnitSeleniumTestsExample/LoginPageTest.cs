using Components.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_NUnitSeleniumTestsExample
{
    [TestFixture]
    public class LoginPageTest : BaseTest
    {
        [Test]
       
        public void LogIntoSystem()
        {
            CurrentPage.As<Login>()
                    .SetEmail(Username)
                    .SetPassword(Password)
                    .ClickSignIn();
            //TpDo Implements GetInstance() method...
            //Assert.That(CurrentPage.CurrentPath, Does.Contain(GetInstance<Home>().PagePath),
            //    "No Redirection after login");
        }

        [Test]
       
        public void EnterIncorrectEmail()
        {
            CurrentPage.As<Login>()
                .SetEmail("john@")
                .SetPassword(Password)
                .ClickSignIn();

            Assert.That(CurrentPage.As<Login>().GetErrorEmailText(), Is.EqualTo("The Email field is not a valid e-mail address."),
                "Email error message is incorrect");
        }

        [Test]      
        public void EnterIncorrectPassword()
        {
            CurrentPage.As<Login>()
                .SetEmail(Username)
                .SetPassword("IncorrectPass")
                .ClickSignIn();

            Assert.That(CurrentPage.As<Login>().GetInvalidLoginErrorLabelText(), Is.EqualTo("Invalid login attempt."),
                "Email error message is incorrect");
        }
    }
}
