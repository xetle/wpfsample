using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Model;
using Microsoft.Practices.Prism.Events;

namespace Owlsure.Events
{
    public class CounterpartyChangeEvent : CompositePresentationEvent<Counterparty> { }
}
