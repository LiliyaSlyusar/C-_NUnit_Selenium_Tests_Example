using Components;
using Components.Pages;
using Core.Driver;
using Core.Helpers;
using LoggerService;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_NUnitSeleniumTestsExample
{
    public abstract class BaseTest
    {
        protected BasePage CurrentPage { get; set; }
        protected string BaseUrl = ConfigurationHelper.Get<string>("BaseUrl");
        protected virtual string Username => ConfigurationHelper.Get<string>("Username");

        protected virtual string Password => ConfigurationHelper.Get<string>("Password");
        #region Setup & Teardown
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            try
            {
                DoTestFixtureSetup();
            }
            catch (Exception ex)
            {
                LoggerManager.LogError(ex.ToString());
                WebDriver.Reset();
            }
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            try
            {
                DoTestFixtureTearDown();
            }
            catch (Exception ex)
            {
                LoggerManager.LogError(ex.ToString());
            }
        }

        [SetUp]
        public void LoginIntoApp()
        {
            Login(Username, Password);
        }

        [TearDown]
        public void LogoutFromApp()
        {
            CurrentPage = PageNavigation.NavigateTo<Login>(CurrentPage.LogOut);
            WebDriver.Reset();
        }

        protected virtual void DoTestFixtureSetup()
        {

        }

        protected virtual void DoTestFixtureTearDown()
        {

        }

        protected virtual void Login(string email, string password)
        {
            CurrentPage = PageNavigation.NavigateToWithRedirection<Login>(BaseUrl).LoginToApp(email, password);
        }
        #endregion

        #region TestExecutionLogActionAttribute
        [SetUp]
        public void BeforeTest()
        {
            try
            {
                var message = TestContext.CurrentContext.Test.Name + ": Test Started";
                LoggerManager.LogInfo(message);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached) Debugger.Break();
                LoggerManager.LogError(ex.ToString());
            }
        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var message = TestContext.CurrentContext.Test.Name + ": Test " +
                              TestContext.CurrentContext.Result.Outcome.Status;

                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                {
                    LoggerManager.LogInfo(message);
                }
                else
                {
                    LoggerManager.LogError(message);
                }
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached) Debugger.Break();
                LoggerManager.LogError(ex.ToString());
            }
        }

        #endregion
    }


    public abstract class BaseTest<TPage> : BaseTest where TPage : BasePage
    {
        protected new TPage CurrentPage { get; set; }
    }
}

