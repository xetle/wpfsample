using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Model;

namespace Owlsure.Interfaces
{
    /// <summary>
    /// This interface is responsible for returning model INPC objects. The concrete class needs to construct these from POCO
    /// objects from the Entity Framework by calling into the data repository layer
    /// </summary>
    public interface ICounterpartyService
    {
        List<Counterparty> FindAll();

        void Add(Counterparty counterparty);
    }
}
