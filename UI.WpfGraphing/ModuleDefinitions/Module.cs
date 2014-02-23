using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Owlsure.UI.WpfGraphing.Views;
using Microsoft.Practices.Unity;

namespace Owlsure.UI.WpfGraphing.ModuleDefinitions
{
    public class Module: IModule
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

            GraphingView view = container.Resolve<GraphingView>();
            region.Add(view, Globals.ViewNames.GraphingView);
        }
    }
}
