using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owlsure.Interfaces;
using System.Collections.ObjectModel;
using Owlsure.Model;
using Microsoft.Practices.Prism.Events;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Owlsure.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Regions;
using System.Diagnostics;

namespace Owlsure.UI.WpfCounterparty.ViewModels
{
    public class CounterpartyViewModel : NotificationObject, IConfirmNavigationRequest
    {
        private ICounterpartyService dataService;
        private IEventAggregator eventAggregator;
        private ObservableCollection<Counterparty> counterparties;
        private ICommand selectionChangedCommand;
        private ICommand addCommand;
        private ICommand saveCommand;

        public CounterpartyViewModel(
            ICounterpartyService dataService,
            IEventAggregator eventAggregator)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            this.counterparties = new ObservableCollection<Counterparty>(dataService.FindAll());

            this.addCommand = new DelegateCommand(() =>
            {
                NewCounterparty = Counterparty.CreateNewCounterparty();
            });

            this.saveCommand = new DelegateCommand(() =>
            {
                dataService.Add(NewCounterparty);
                this.Counterparties = new ObservableCollection<Counterparty>(dataService.FindAll());
                RaisePropertyChanged("Counterparties");
            });

            if (counterparties.Count() > 0)
            {
                SelectedCounterparty = counterparties.First();
                CounterpartyChangeEvent evt = eventAggregator.GetEvent<CounterpartyChangeEvent>();
                evt.Publish(SelectedCounterparty);
            }
        }
        
        public ObservableCollection<Counterparty> Counterparties
        {
            get { return counterparties; }
            set { counterparties = value; }
        }

        public ICommand SelectionChangedCommand
        {
            get
            {
                if (selectionChangedCommand == null)
                {
                    selectionChangedCommand = new DelegateCommand<Counterparty>(OnCounterpartyChanged);
                }
                return selectionChangedCommand;
            }
        }

        public ICommand AddCommand
        {
            get { return addCommand; }
            set { addCommand = value; }
        }

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }

        void OnCounterpartyChanged(Counterparty newCounterparty)
        {
            if (newCounterparty != null)
            {
                SelectedCounterparty = newCounterparty;
                CounterpartyChangeEvent evt = eventAggregator.GetEvent<CounterpartyChangeEvent>();
                evt.Publish(newCounterparty);
            }
        }

        private Counterparty selectedCounterparty;

        public Counterparty SelectedCounterparty
        {
            get { return selectedCounterparty; }
            set 
            { 
                selectedCounterparty = value;
                RaisePropertyChanged("SelectedCounterparty");
            }
        }

        private Counterparty newCounterparty;
        public Counterparty NewCounterparty
        {
            get { return newCounterparty; }
            set
            {
                newCounterparty = value;
                RaisePropertyChanged("NewCounterparty");
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
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
}
