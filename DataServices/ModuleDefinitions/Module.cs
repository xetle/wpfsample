using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using Owlsure.Interfaces;

namespace Owlsure.DataServices.ModuleDefinitions
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
            // Create a single instance of the services so their model objects can be shared between view models

            //container.RegisterType<ICounterpartyService, CounterpartyService>();
            //container.RegisterType<IUsageService, UsageService>();

            var counterpartyService = container.Resolve<CounterpartyService>();
            container.RegisterInstance<ICounterpartyService>(counterpartyService);

            var usageService = container.Resolve<UsageService>();
            container.RegisterInstance<IUsageService>(usageService);
        
        }
    }
}
