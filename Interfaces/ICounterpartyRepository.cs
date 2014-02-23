using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.EFDataLayer;

namespace Owlsure.Interfaces
{
    /// <summary>
    /// Not totally sure about this interface. The concrete implementation uses EF. This interface returns POCOs (Entities) and is called by the
    /// Service layer 
    /// </summary>
    public interface ICounterpartyRepository
    {
        List<EFDataLayer.Counterparty> ListAll();
    }
}
