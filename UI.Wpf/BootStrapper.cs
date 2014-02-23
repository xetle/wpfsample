using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Owlsure.DataServices;
using Owlsure.Interfaces;
using Microsoft.Practices.Unity;

namespace Owlsure.UI.Wpf
{
    public class BootStrapper: UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
            //return (Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
            //  new Uri("ModuleCatalog.xaml", UriKind.Relative)));
        }
        protected override System.Windows.DependencyObject CreateShell()
        {
            var menuService = Container.Resolve<MenuService>();
            Container.RegisterInstance<IMenuService>(menuService);

            MainWindow window = Container.TryResolve<MainWindow>();
            window.Show();

            return window;
        }
    }
}
