using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.IO;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration;

namespace Cdts.Framework
{
    public static class ManagerFactory
    {

        //单件模式中采用双重锁定对 Instance 进行初始化
        private static IUnityContainer container = null;
        private static readonly object containerlock = new object();
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    lock (containerlock)
                    {
                        if (container == null)
                        {
                            //读取配置文件
                            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\FrameworkUnity.config");
                            if (!File.Exists(path))
                            {
                                throw new FileNotFoundException(path);
                            }
                            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                            fileMap.ExeConfigFilename = path;
                            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                            container = new UnityContainer();
                            UnityConfigurationSection section = (UnityConfigurationSection)config.Sections["unity"];
                            section.Configure(container, "Controllers");
                        }
                    }
                }
                return container;
            }
        }
        public static TManager Create<TManager>()
        {
            string name = typeof(TManager).Name;
            name = name.Substring(name.LastIndexOf(".") + 1);

            return Container.Resolve<TManager>(name);
        }

        public static object Create(Type type)
        {
            string name = type.Name;
            name = name.Substring(name.LastIndexOf(".") + 1);
            return Container.Resolve(type, name);
        }
    }
}
