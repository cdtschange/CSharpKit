using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Cdts.Web.Mvc
{
    public class UnityServiceLocator : ServiceLocatorImplBase
    {
        private IUnityContainer container;
        public UnityServiceLocator(IUnityContainer container)
        {
            this.container = container;
        }
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return container.Resolve(serviceType, key);
        }
    }
}
