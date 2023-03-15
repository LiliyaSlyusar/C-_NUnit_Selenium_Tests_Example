using Components.Pages;
using HtmlElements.Elements;

namespace CS_NUnitSeleniumTestsExample
{
    [TestFixture]
    public class ContactUsPageTests : BaseTest
    {
        protected override string Username => "admin@dev.com";

        protected override void DoTestFixtureSetup()
        {
            //ToDo: Setup data for user from DB or Rest API
            // Code...
        }

        [Test]

        public void ContactUsSentContactRequest()
        {
            CurrentPage = CurrentPage.As<Home>().NavBar.ClickContactLink();

            CurrentPage.As<ContactUs>().SendRequest(Username, Username, "555-555-666", "This is a test request");

            Assert.IsTrue(CurrentPage.As<ContactUs>().IsResultMessagePresent(), "Result message is not present");
        }

        [Test]
        
        public void ContactUsViewContactMenu()
        {
            if (CurrentPage is Home)
            {
                CurrentPage = CurrentPage.As<Home>().NavBar.ClickContactLink();
            }
            else
            {
                CurrentPage.As<ContactUs>()
                    .NavBar
                    .ClickHelpLink()
                    .NavBar
                    .ClickContactLink();
            }

            Assert.IsTrue(CurrentPage.As<ContactUs>().IsElementsPresent(), "Target Elements are not present on Contact Us page");
        }
    }
}
