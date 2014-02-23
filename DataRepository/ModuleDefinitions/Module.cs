using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Owlsure.Interfaces;

namespace Owlsure.DataRepository.ModuleDefinitions
{
    public class Module: IModule
    {
        private IUnityContainer container;

        public Module(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<ICounterpartyRepository, CounterpartyRepository>();
            container.RegisterType<IUsageRepository, UsageRepository>();
        }
    }
}
