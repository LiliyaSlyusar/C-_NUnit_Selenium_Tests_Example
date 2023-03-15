using Core.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class TempChangeSearchTimeout : IDisposable
    {
        public TempChangeSearchTimeout(TimeSpan timeSpan)
        {
            WebDriver.GetDriver().SetDefaultObjectSearchTimeout(timeSpan);
        }

        public void Dispose()
        {
            WebDriver.GetDriver().SetDefaultObjectSearchTimeout(TimeSpan.FromSeconds(ConfigurationHelper.Get<double>("DefaultWait")));
        }
    }
}
