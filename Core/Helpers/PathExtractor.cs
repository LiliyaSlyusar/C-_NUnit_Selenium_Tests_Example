using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    /// <summary>
    /// Provides methods for extracting file paths from the output directory and deserializing JSON files.
    /// </summary>
    public static class PathExtractor
    {
        /// <summary>
        /// Gets the absolute file path for a file located in the output directory, given a relative path.
        /// </summary>
        /// <param name="relativePath">The relative path of the file, starting from the output directory.</param>
        /// <returns>The absolute file path of the file.</returns>
        public static string GetPathFromOutputDir(string relativePath)
        {
            var uriString = Assembly.GetExecutingAssembly().CodeBase;
            var localPath = new Uri(uriString).LocalPath;
            var directoryName = Path.GetDirectoryName(localPath);
            return Path.Combine(directoryName, relativePath.TrimStart('\\'));
        }

        /// <summary>
        /// Deserializes a JSON file located in the output directory, given its relative path.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the JSON into.</typeparam>
        /// <param name="relativePath">The relative path of the JSON file, starting from the output directory.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string relativePath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(GetPathFromOutputDir(relativePath)));
        }
    }
}
