 
 
 
 
using System;
using System.Resources;
using System.IO;

namespace Cdts.Web
{
	internal class Resources
	{
		static ResourceManager resourceManager;
		internal static ResourceManager ResourceManager 
		{
            get {
                if (object.ReferenceEquals(resourceManager, null)) {
					string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\Resources");
                    ResourceManager temp = ResourceManager.CreateFileBasedResourceManager("Cdts.Web", path, null);
                    resourceManager = temp;
                }
                return resourceManager;
            }
        }
		static System.Globalization.CultureInfo resourceCulture;
		internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
		internal static string Resource(string name,params object[] args)
		{
			return string.Format(ResourceManager.GetString(name, resourceCulture), args);
		}
	}
}
