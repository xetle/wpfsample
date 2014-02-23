using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Owlsure.Globals;
using Owlsure.UI.WpfUsage.Views;
using Microsoft.Practices.Unity;

namespace Owlsure.UI.WpfUsage.ModuleDefinitions
{
    class Module: IModule
    {
        private IRegionManager regionManager;
        private IUnityContainer container;

        public Module(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            var region = regionManager.Regions[Globals.RegionNames.RightRegion];

            UsageView view = container.Resolve<UsageView>();
            region.Add(view, Globals.ViewNames.UsageView);
        }
    }
}
