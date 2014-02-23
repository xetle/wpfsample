using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using Owlsure.Interfaces;

namespace Owlsure.DataRepository.ModuleDefinitions
{
    public class MockModule: IModule
    {
        private IUnityContainer container;

        public MockModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<ICounterpartyRepository, MockCounterpartyRepository>();
            container.RegisterType<IUsageRepository, MockUsageRepository>();
        }
    }
}
