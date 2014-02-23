using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Owlsure.UI.WpfUsageDaily.Views;

namespace Owlsure.UI.WpfUsageDaily.ModuleDefinitions
{
    public class Module: IModule
    {
        IRegionManager regionManager;
        IUnityContainer container;

        public Module(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            // This is done because I want to use RequestNavigate to a popup region containing this view
            container.RegisterType<Object, UsageDailyView>(Globals.ViewNames.UsageDailyViewPopup);

            var region = regionManager.Regions[Globals.RegionNames.RightRegion];

            var view = container.Resolve<UsageDailyView>(Globals.ViewNames.UsageDailyView);
            region.Add(view, Globals.ViewNames.UsageDailyView);

            var viewGraph = container.Resolve<UsageDailyGraphView>();
            region.Add(viewGraph, Globals.ViewNames.UsageDailyGraphView);
        }
    }
}
