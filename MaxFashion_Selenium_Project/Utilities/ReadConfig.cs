using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.Utilities
{
    internal class ReadConfig
    {
        public static Dictionary<string, string>? properties;
        public static void ReadConfigSettings()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            properties = new Dictionary<string, string>();

            string fileName = currDir + "/configsettings/config.properties";
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;
                }

            }
        }
    }
}
