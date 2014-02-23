using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using Owlsure.UI.WpfGraphing.Views;
using Owlsure.Interfaces;
using System.Collections.ObjectModel;

namespace Owlsure.UI.Wpf
{
    public class MainWindowViewModel: INavigationAware
    {
        private IRegionManager regionManager;
        private IUnityContainer container;
        private IMenuService menuService;

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer container, IMenuService menuService)
        {
            this.regionManager = regionManager;
            this.container = container;
            this.menuService = menuService;

            this.menuEntries = menuService.MenuEntries;
        }

        private ObservableCollection<MenuEntry> menuEntries;

        public ObservableCollection<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
            set { menuEntries = value; }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug.WriteLine("OnNavigatedFrom");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Debug.WriteLine("OnNavigatedTo");
        }

        void Callback(NavigationResult result)
        {
            Debug.WriteLine("Callback");
        }

    }
}
