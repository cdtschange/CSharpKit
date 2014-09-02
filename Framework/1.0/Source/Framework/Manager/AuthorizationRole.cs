using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Cdts.Core;

namespace Cdts.Framework
{
    public class AuthorizationRole : AuthorizationBase
    {
        public AuthorizationRole()
        {
            string authorizationConfig = ConfigurationManager.AppSettings["AuthorizationConfig"];
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, authorizationConfig);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = path;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            KeyValueConfigurationCollection collection = config.AppSettings.Settings;
            AuthorizationDictionary.Clear();
            foreach (KeyValueConfigurationElement keyValue in collection)
            {
                AuthorizationDictionary.Add(keyValue.Key, keyValue.Value);
            }
        }
        public override bool Authorization(string authorizationName, IUser user)
        {
            string auth = GetValueByKey(authorizationName);
            List<string> roles = auth.Split<string>(",");
            List<string> userRoles = user.Roles.Select(r => r.Name).ToList();
            return roles.Intersect(userRoles).Count() > 0;
        }
    }
}
