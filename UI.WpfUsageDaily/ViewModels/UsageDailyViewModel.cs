using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Owlsure.Interfaces;
using Owlsure.Events;
using Owlsure.Model;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace Owlsure.UI.WpfUsageDaily.ViewModels
{
    public class UsageDailyViewModel: NotificationObject
    {
        IEventAggregator eventAggregator;
        IUsageService usageService;
        IMenuService menuService;
        IRegionManager regionManager;

        public UsageDailyViewModel(IEventAggregator eventAggregator, IUsageService usageService, IMenuService menuService, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.usageService = usageService;
            this.menuService = menuService;
            this.regionManager = regionManager;

            CounterpartyChangeEvent evt = eventAggregator.GetEvent<CounterpartyChangeEvent>();
            evt.Subscribe(OnSelectedCounterpartyChanged);

            // We may be launching this view after the counterparty has been selected e.g. popup window so get the usage for the currently selected counterparty
            if (usageService.CurrentCounterparty != null)
            {
                var usages = usageService.FindLatestUsageByCounterparty(usageService.CurrentCounterparty);
                Usages = new ObservableCollection<Model.Usage>(usages);
            }

            showUsageDailyCommand = new DelegateCommand(ShowUsageDaily);

            menuService.RegisterMenu(new MenuEntry() { Text = "Daily Usage", Command = showUsageDailyCommand });

            showGraphCommand = new DelegateCommand(ShowGraph);

            menuService.RegisterMenu(new MenuEntry() { Text = "Show Graph", Command = showGraphCommand });

            showUsageDailyPopupCommand = new DelegateCommand(ShowUsageDailyPopup);
            menuService.RegisterMenu(new MenuEntry() { Text = "Show PopUp Daily Usage", Command = showUsageDailyPopupCommand });
        }

        void OnSelectedCounterpartyChanged(Counterparty newCounterparty)
        {
            var usages = usageService.FindLatestUsageByCounterparty(newCounterparty);
            Usages = new ObservableCollection<Model.Usage>(usages);
        }

        private ObservableCollection<Usage> usages;

        public ObservableCollection<Usage> Usages
        {
            get { return usages; }
            set 
            { 
                usages = value;
                RaisePropertyChanged("Usages");
            }
        }

        private ICommand showUsageDailyCommand;

        public ICommand ShowUsageDailyCommand
        {
            get { return showUsageDailyCommand; }
            set { showUsageDailyCommand = value; }
        }

        void ShowUsageDaily()
        {
            var region = regionManager.Regions[Globals.RegionNames.RightRegion];

            var view = region.GetView(Globals.ViewNames.UsageDailyView);
            region.Activate(view);
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

        private ICommand showUsageDailyPopupCommand;

        public ICommand ShowUsageDailyPopupCommand
        {
            get { return showUsageDailyPopupCommand; }
            set { showUsageDailyPopupCommand = value; }
        }

        void ShowUsageDailyPopup()
        {
            var view = new Uri(Globals.ViewNames.UsageDailyViewPopup, UriKind.Relative);
            this.regionManager.RequestNavigate(Globals.RegionNames.PopupRegion, view);
        }

    }
}
