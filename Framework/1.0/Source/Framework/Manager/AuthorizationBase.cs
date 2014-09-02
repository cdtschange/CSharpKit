using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public abstract class AuthorizationBase : IAuthorization
    {
        private static Dictionary<string, string> authorizationDictionary;
        public static Dictionary<string, string> AuthorizationDictionary
        {
            get
            {
                if (authorizationDictionary == null)
                {
                    authorizationDictionary = new Dictionary<string, string>();
                }
                return authorizationDictionary;
            }
        }

        public abstract bool Authorization(string authorizationName, IUser user);

        protected string GetValueByKey(string key)
        {
            if (AuthorizationDictionary.ContainsKey(key))
            {
                return AuthorizationDictionary[key];
            }
            string value = "";
            if (key.Contains('.'))
            {
                string parent = key.Substring(0, key.LastIndexOf('.'));
                value = GetValueByKey(parent);
                if (!string.IsNullOrEmpty(value))
                {
                    AuthorizationDictionary.Add(key, value);
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException(string.Format("{0}：没有对应的权限验证信息", key));
            }
            return value;
        }



    }
}
