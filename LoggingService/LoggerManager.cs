using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService
{
    public static class LoggerManager
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        public static void LogInfo(object message)
        {
            Logger.Info(message);
            Debug.WriteLine(message);
        }

        public static void LogError(object message)
        {
            Logger.Error(message);
            Debug.WriteLine(message);
        }

        public static void LogDebug(object message)
        {
            Logger.Debug(message);
            Debug.WriteLine(message);
        }
    }
}
