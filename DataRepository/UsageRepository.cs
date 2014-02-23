using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Interfaces;
using Owlsure.EFDataLayer;

namespace Owlsure.DataRepository
{
    public class UsageRepository: IUsageRepository
    {
        public List<Usage> ListUsageByCounterparty(int counterpartyId)
        {
            using (var context = new EFDataLayer.EFEntities())
            {
                DateTime dt = DateTime.Today.Subtract(new TimeSpan(365,0,0,0)); // get 1 years exposure
                return context.Usages.Where(u => u.CounterpartyId == counterpartyId).Where(d => d.ExposureDate > dt).OrderByDescending(u => u.ExposureDate).ToList();
            }
        }
    }

    public class MockUsageRepository : IUsageRepository
    {
        public List<Usage> ListUsageByCounterparty(int counterpartyId)
        {
            List<EFDataLayer.Usage> usages = new List<EFDataLayer.Usage>();

            const int numDateSteps = 365;
            int numDataTypes = (new Random(counterpartyId)).Next(5, 10);

            Random random = new Random(counterpartyId);

            for (int dateStep = 0; dateStep < numDateSteps; dateStep++)
            {
                for (int j = 0; j < numDataTypes; j++)
                {
                    EFDataLayer.Usage usage = new Usage();

                    usage.Id = dateStep * numDataTypes + j;
                    usage.ExposureDate = DateTime.Today.Subtract(new TimeSpan(dateStep,0,0,0));
                    usage.TradingLine = string.Format("TradingLine{0}", j);
                    usage.Exposure = random.NextDouble() * 10.0E6 + 1.0E6; // between 1M & 11M
                    usage.CounterpartyId = counterpartyId;
                    usages.Add(usage);
                }
            }

            return usages;
        }
    }
}
