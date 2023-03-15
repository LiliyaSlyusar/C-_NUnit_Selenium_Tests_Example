using Components.CommonComponents;
using Core.Driver;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class BasePage
    {
        /// <summary>
        /// Page title is used in page navigation to validate that the Driver navigated to the correct page.
        /// </summary>
        public virtual string PageTitle => WebDriver.GetDriver().Title ?? string.Empty;

        /// <summary>
        /// Page path is used by the Driver to navigate to the page.
        /// </summary>
        public virtual string PagePath => string.Empty;

        /// <summary>
        /// Current Page path detected by the Driver to compair with expected PagePath.
        /// </summary>
        public string CurrentPath => WebDriver.GetDriver().Url;


        /// <summary>
        /// Page URL is used by the Driver to navigate to the page. This can be overridden for System Center pages.
        /// </summary>
        public virtual string PageUrl => ConfigurationHelper.Get<string>("BaseUrl") + PagePath;

        public TopBarComponent NavBar { get; set; }

        public void LogOut()
        {

        }

        #region Dynamic Page Type Handling
        public TPage As<TPage>() where TPage :BasePage, new()
        {
            return (TPage)this;
        }
        #endregion

    }
}
