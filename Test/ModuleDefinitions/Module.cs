using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Owlsure.Globals;

namespace Test.ModuleDefinitions
{
    public class Module: IModule
    {
        public Module(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        IRegionManager regionManager;

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.LeftRegion, typeof(Views.Test));
        }
    }
}
