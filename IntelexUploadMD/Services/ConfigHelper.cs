using IntelexUploadMD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelexUploadMD.Services
{
    public class ConfigHelper
    {
        /// Helper method to load configuration from a JSON file
        public static ConfigDataSet LoadConfig(string configFile)
        {
            try
            {
                string jsonContent = File.ReadAllText(configFile);
                return System.Text.Json.JsonSerializer.Deserialize<ConfigDataSet>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                return null;
            }
        }
    }
}
