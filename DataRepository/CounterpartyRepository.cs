using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Interfaces;
using Owlsure.EFDataLayer;

namespace Owlsure.DataRepository
{
    public class CounterpartyRepository : ICounterpartyRepository
    {
        public List<EFDataLayer.Counterparty> ListAll()
        {
            using (var context = new EFDataLayer.EFEntities())
            {
                return context.Counterparties.OrderBy(c => c.Name).ToList();
            }
        }

        public int Add(EFDataLayer.Counterparty counterparty)
        {
            using (var context = new EFDataLayer.EFEntities())
            {
                context.Counterparties.Add(counterparty);
                context.SaveChanges();
                return counterparty.Id;
            }
        }
    }

    public class MockCounterpartyRepository : ICounterpartyRepository
    {
        private List<EFDataLayer.Counterparty> counterparties;

        public MockCounterpartyRepository()
        {
            counterparties = new List<EFDataLayer.Counterparty>();
            for (int i = 0; i < 100; i++)
            {
                EFDataLayer.Counterparty counterparty = new Counterparty();

                counterparty.Id = i;
                counterparty.Code = string.Format("MEGA{0}", i);
                counterparty.Name = string.Format("Mega Bank{0}", i);
                counterparties.Add(counterparty);
            }

        }

        public List<EFDataLayer.Counterparty> ListAll()
        {
            return counterparties;
        }

        public int Add(EFDataLayer.Counterparty counterparty)
        {
            counterparty.Id = counterparties.Count() + 1;
            counterparties.Add(counterparty);

            return counterparty.Id;
        }
    }
}
