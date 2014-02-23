using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Interfaces;
using Owlsure.Model;

namespace Owlsure.DataServices
{
    public class CounterpartyService : ICounterpartyService
    {
        ICounterpartyRepository counterpartyRepository;

        // The repository is dependency injected
        public CounterpartyService(ICounterpartyRepository counterpartyRepository)
        {
            this.counterpartyRepository = counterpartyRepository;
        }

        public List<Counterparty> FindAll()
        {
            List<Counterparty> counterparties = new List<Counterparty>();

            var cpList = counterpartyRepository.ListAll();

            foreach(EFDataLayer.Counterparty cp in cpList)
            {
                Counterparty counterparty = Counterparty.CreateNewCounterparty();
                counterparty.Id = cp.Id;
                counterparty.Name = cp.Name;
                counterparty.Code = cp.Code;

                counterparties.Add(counterparty);
            }

            return counterparties;
        }

        public void Add(Counterparty counterparty)
        {
            throw new NotImplementedException();
        }
    }
}
