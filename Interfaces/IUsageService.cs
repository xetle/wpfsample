using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Model;
using System.Collections.ObjectModel;

namespace Owlsure.Interfaces
{
    public interface IUsageService
    {
        List<Usage> FindByCounterparty(Counterparty counterparty);
        List<Usage> FindLatestUsageByCounterparty(Counterparty counterparty);
        Counterparty CurrentCounterparty { get; }
    }
}
