using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using System.Diagnostics;
using Owlsure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace Owlsure.UI.WpfGraphing.ViewModels
{
    public class GraphingViewModel: INavigationAware
    {
        private IMenuService menuService;
        IRegionManager regionManager;

        public GraphingViewModel(IMenuService menuService, IRegionManager regionManager)
        {
            this.menuService = menuService;
            this.regionManager = regionManager;

            this.showGraphCommand = new DelegateCommand(ShowGraph);

            menuService.RegisterMenu(new MenuEntry() { Text = "Show Graph", Command = showGraphCommand });
        }

        private ICommand showGraphCommand;

        public ICommand ShowGraphCommand
        {
            get { return showGraphCommand; }
            set { showGraphCommand = value; }
        }

        void ShowGraph()
        {
            var region = regionManager.Regions[Globals.RegionNames.RightRegion];
            Uri view = new Uri(Globals.ViewNames.UsageDailyGraphView, UriKind.Relative);
            region.RequestNavigate(view, CheckForError);
        }

        void CheckForError(Microsoft.Practices.Prism.Regions.NavigationResult nr)
        {
            if (nr.Result == false)
            {
                throw new Exception(nr.Error.Message);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug.WriteLine("OnNavigatedFrom");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Debug.WriteLine("OnNavigatedTo");
        }
    }
}
