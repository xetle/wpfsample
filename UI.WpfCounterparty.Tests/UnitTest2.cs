using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owlsure.UI.WpfCounterparty.ViewModels;
using Microsoft.Practices.Unity;
using Owlsure.Interfaces;
using Owlsure.DataServices;
using Owlsure.DataRepository;
using Owlsure.Model;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;

using Moq;
using Owlsure.Events;
using System.Windows.Input;


namespace UI.WpfCounterparty.Tests
{
    [TestClass]
    public class CounterpartyTest
    {
        [TestMethod]
        public void TotalNumberOfCounterpartiesShouldBeTwo()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ICounterpartyRepository, MockCounterpartyRepository>();
            container.RegisterType<ICounterpartyService, CounterpartyService>();
            container.RegisterType<IEventAggregator, EventAggregator>();

            CounterpartyViewModel vm = container.Resolve<CounterpartyViewModel>();

            Assert.AreEqual(100, vm.Counterparties.Count());
        }

        [TestMethod]
        public void AnEventIsPublishedWhenTheSelectedCounterpartyChanges()
        {
            var mockEventAggregator = new Mock<IEventAggregator>();
            var mockCounterpartyService = new Mock<ICounterpartyService>();

            mockCounterpartyService.Setup(s => s.FindAll()).Returns(new List<Counterparty>());
            mockEventAggregator.Setup(s => s.GetEvent<CounterpartyChangeEvent>()).Returns(new CounterpartyChangeEvent());

            var mockCounterparty = new Mock<Counterparty>();
            mockCounterparty.Object.Id = 1;
            mockCounterparty.Object.Code = "Test";

            CounterpartyViewModel vm = new CounterpartyViewModel(mockCounterpartyService.Object, mockEventAggregator.Object);

            vm.SelectedCounterparty = mockCounterparty.Object;

            ICommand cmd = vm.SelectionChangedCommand;
            cmd.Execute(mockCounterparty.Object);

            mockEventAggregator.Verify(e => e.GetEvent<CounterpartyChangeEvent>(), Times.Once());

        }
    
    }
}
