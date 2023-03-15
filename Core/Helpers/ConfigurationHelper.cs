using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static  class ConfigurationHelper
    {
        /// <summary>
        /// Retrieves the value of an application setting from the configuration file and returns it as an instance of type T.
        /// </summary>
        /// <typeparam name="T">The data type to convert the application setting value to.</typeparam>
        /// <param name="name">The name of the application setting to retrieve.</param>
        /// <returns>An instance of type T that represents the value of the specified application setting.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the name parameter is null or empty.</exception>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Thrown if the application setting with the specified name is not found.</exception>
        /// <exception cref="System.InvalidCastException">Thrown if the application setting value cannot be converted to type T.</exception>
        /// <exception cref="System.FormatException">Thrown if the application setting value is not in the correct format for type T.</exception>
        /// <exception cref="System.OverflowException">Thrown if the application setting value is outside the range of valid values for type T.</exception>
       
        public static T Get<T>(string name)
        {
            var value = ConfigurationManager.AppSettings[name];
            Assert.IsNotNull(value, $"AppSettings with name: {name} not found. Please check the applciation configuration");

            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }
        /// <summary>
        /// Retrieves the connection string with the specified name from the configuration file.
        /// </summary>
        /// <param name="name">The name of the connection string to retrieve.</param>
        /// <returns>A string that contains the connection string for the specified name.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the name parameter is null or empty.</exception>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Thrown if the connection string with the specified name is not found.</exception>
        
        public static string GetConnectionString(string name)
        {
            var value = ConfigurationManager.ConnectionStrings[name];
            Assert.IsNotNull(value, $"ConnectionString with name: {name} not found. Please checke the application configuration");
            return value.ConnectionString;
        }
    }
}
