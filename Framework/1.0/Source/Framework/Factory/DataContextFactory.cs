﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cdts.Core;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Cdts.Framework
{
    public sealed class DataContextFactory : IDataContextFactory<Guid>
    {
        //单件模式中采用双重锁定对 Instance 进行初始化
        private static IUnityContainer container = null;
        private static readonly object containerlock = new object();
        private static IUnityContainer Container
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
                            section.Configure(container, "DataContext");
                        }
                    }
                }
                return container;
            }
        }



        public static IDataContext<Guid> Create()
        {
            return Container.Resolve<IDataContext<Guid>>("IDataContext");
        }

        public static IDataTransaction CreateDataTransaction()
        {
            return Container.Resolve<IDataTransaction>();
        }
    }
}
