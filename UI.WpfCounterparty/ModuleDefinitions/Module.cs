using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Owlsure.UI.WpfCounterparty.Views;
using Owlsure.Globals;

namespace Owlsure.UI.WpfCounterparty.ModuleDefinitions
{
    public class Module: IModule
    {
        public Module(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.LeftRegion, typeof(CounterpartyView)); ;
        }

        private IRegionManager regionManager;
    }
}
