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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Owlsure.UI.WpfUsage.ViewModels;

namespace Owlsure.UI.WpfUsage.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UsageView : UserControl
    {
        public UsageView(UsageViewModel viewModel)
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }

        public string Header { get { return "Usage"; } }
    }
}
