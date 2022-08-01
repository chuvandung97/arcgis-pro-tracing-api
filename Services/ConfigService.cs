using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Schema.Web.Services
{
    public class ConfigService : IConfigService
    {
        public string GetAppSetting(string name)
        {
            if (ConfigurationManager.AppSettings[name] != null)
                return ConfigurationManager.AppSettings[name].ToString();
            else
                throw new Exception("Application Config '" + name + "' not found.");
        }

        public string GetConnectionString(string name)
        {
            if (ConfigurationManager.ConnectionStrings[name] != null)
                return ConfigurationManager.ConnectionStrings[name].ToString();
            else
                throw new Exception("Connectionstring '" + name + "' not found.");
        }
    }
}