using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Owlsure.Events;
using Owlsure.Model;
using Owlsure.Interfaces;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Regions;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace Owlsure.UI.WpfUsage.ViewModels
{
    public class UsageViewModel : NotificationObject, IConfirmNavigationRequest
    {
        private IEventAggregator eventAggregator;
        private IUsageService usageService;
        private IMenuService menuService;
        private IRegionManager regionManager;

        public UsageViewModel(IEventAggregator eventAggregator, IUsageService usageService, IMenuService menuService, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.usageService = usageService;
            this.menuService = menuService;
            this.regionManager = regionManager;

            CounterpartyChangeEvent counterpartyChangeEvent = eventAggregator.GetEvent<CounterpartyChangeEvent>();
            counterpartyChangeEvent.Subscribe(OnCounterpartyChanged);

            showUsageCommand = new DelegateCommand(ShowUsage);

            menuService.RegisterMenu(new MenuEntry() { Text = "Usage", Command = showUsageCommand });
        }

        private ICommand showUsageCommand;

        public ICommand ShowUsageCommand
        {
            get { return showUsageCommand; }
            set { showUsageCommand = value; }
        }

        void ShowUsage()
        {
            var region = regionManager.Regions[Globals.RegionNames.RightRegion];
            var view = region.GetView(Globals.ViewNames.UsageView);
            region.Activate(view);
        }

        void OnCounterpartyChanged(Counterparty newCounterparty)
        {
            var usage = usageService.FindByCounterparty(newCounterparty);

            var pivotedUsageData = new PivotedUsageData(usage);
            Usage = pivotedUsageData;

            CounterpartyName = newCounterparty.Name;
        }

        private string counterpartyName;

        public string CounterpartyName
        {
            get { return counterpartyName; }
            set { 
                counterpartyName = value;
                RaisePropertyChanged("CounterpartyName");
            }
        }

        private PivotedUsageData usage;

        public PivotedUsageData Usage
        {
            get
            {
                return usage;
            }
            set
            {
                usage = value;
                RaisePropertyChanged("Usage");
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            // Need to do this to indicate that the view can be navigated away from
            continuationCallback(true);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug.WriteLine(navigationContext.ToString());
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Debug.WriteLine(navigationContext.ToString());
        }
    }

    public class PivotedUsageData
    {
        public PivotedUsageData(IList<Usage> usage)
        {
            UsageRows = new List<Usage[]>();

            // The trading lines are going to appear as the columns
            ColumnNames = usage.Select(u => u.TradingLine).Distinct().OrderBy(o => o).ToList();
            int colCount = ColumnNames.Count();

            // Create a lookup dictionary so we can put the trading line exposure in the correct column
            int i = 0;
            Dictionary<string, int> TradingLineLookup = new Dictionary<string, int>();
            ColumnNames.ForEach(c => TradingLineLookup.Add(c, i++));

            int numRows = usage.Select(c => c.ExposureDate).Distinct().Count();

            var items = new Dictionary<DateTime, Usage[]>();

            foreach (var u in usage)
            {
                Usage[] row;
                if (!items.TryGetValue(u.ExposureDate, out row))
                {
                    row = new Usage[colCount] ;
                    items.Add(u.ExposureDate, row);
                }

                int colIndex = TradingLineLookup[u.TradingLine];
                row[colIndex] = u;
            }

            foreach (var r in items.Values)
            {
                UsageRows.Add(r); 
            }

        }

        public List<string> ColumnNames { get; private set; }
        public List<Usage[]> UsageRows { get; private set; }
    }

    public class DesignTimeUsageViewModel 
    {
        public DesignTimeUsageViewModel()
        {
            List<Usage> usageList = new List<Model.Usage>();

            for (int dateStep = 0; dateStep < 15; dateStep++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var u = Model.Usage.CreateNewUsage();
                    u.TradingLine = string.Format("TradingLine {0}", j);
                    u.Id = 1;
                    u.ExposureDate = DateTime.Today.Subtract(new TimeSpan(dateStep, 0, 0, 0));
                    u.Exposure = 10E6;
                    usageList.Add(u);
                }
            }
            
            usage = new PivotedUsageData(usageList);
        }

        private Owlsure.UI.WpfUsage.ViewModels.PivotedUsageData usage;

        public Owlsure.UI.WpfUsage.ViewModels.PivotedUsageData Usage
        {
            get
            {
                return usage;
            }
            set
            {
                usage = value;
            }
        }

        public string CounterpartyName
        {
            get { return "Mega Bank One"; }
        }
    }
}
