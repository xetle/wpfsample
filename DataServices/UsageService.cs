using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Interfaces;
using Owlsure.Model;
using System.Collections.ObjectModel;
using System.Timers;

namespace Owlsure.DataServices
{
    class UsageService: IUsageService
    {
        // cache the usage for the current counterparty
        List<Usage> usageForCurrentCounterparty;
        Counterparty currentCounterparty;

        // Randomize the 5th counterparty
        Timer timer = new Timer();

        IUsageRepository usageRepository;
        public UsageService(IUsageRepository usageRepository)
        {
            this.usageRepository = usageRepository;

            usageForCurrentCounterparty = new List<Usage>();

            timer.Enabled = false;
            timer.Interval = 3000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Random rnd = new Random();
            foreach (var usage in usageForCurrentCounterparty)
            {
                usage.Exposure = rnd.NextDouble() * 10.0E6 + 1.0E6;
            }
        }

        public List<Usage> FindByCounterparty(Counterparty counterparty)
        {
            if (currentCounterparty == null || counterparty.Id != currentCounterparty.Id)
            {
                usageForCurrentCounterparty = new List<Usage>();

                var efUsageList = usageRepository.ListUsageByCounterparty(counterparty.Id);

                foreach (EFDataLayer.Usage efUsage in efUsageList)
                {
                    Usage usage = Usage.CreateNewUsage();
                    usage.Id = efUsage.Id;
                    usage.Exposure = efUsage.Exposure ?? 0; // coalesce
                    usage.ExposureDate = efUsage.ExposureDate;
                    usage.TradingLine = efUsage.TradingLine;

                    usageForCurrentCounterparty.Add(usage);
                }

                currentCounterparty = counterparty;
            }

            timer.Enabled = currentCounterparty.Id == 5;

            return usageForCurrentCounterparty;
        }

        public List<Usage> FindLatestUsageByCounterparty(Counterparty counterparty)
        {
            List<Usage> usageList = new List<Usage>();

            if (currentCounterparty == null || counterparty.Id != currentCounterparty.Id)
            {
                usageForCurrentCounterparty = FindByCounterparty(counterparty);
            }

            if (usageForCurrentCounterparty.Count() > 0)
            {
                var maxDate = usageForCurrentCounterparty.Select(u => u.ExposureDate).Max();

                usageList = usageForCurrentCounterparty.Where(u => u.ExposureDate == maxDate).ToList();
            }
            return usageList;
        }

        public Counterparty CurrentCounterparty
        {
            get { return currentCounterparty; }
        }
    }
}
