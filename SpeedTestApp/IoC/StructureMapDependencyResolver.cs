using StructureMap;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedTestApp.IoC
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private IContainer container;

        public StructureMapDependencyResolver (IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;
            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

    }
}