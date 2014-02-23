using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Owlsure.UI.WpfCounterparty.ViewModels;

namespace Owlsure.UI.WpfCounterparty.Views
{
    /// <summary>
    /// Interaction logic for CounterpartyView.xaml
    /// </summary>
    public partial class CounterpartyView : UserControl
    {
        /// <summary>
        /// By having the view model in the constructor it can be dependency injected
        /// </summary>
        /// <param name="viewModel"></param>
        public CounterpartyView(CounterpartyViewModel viewModel)
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };

        }

    }
}
