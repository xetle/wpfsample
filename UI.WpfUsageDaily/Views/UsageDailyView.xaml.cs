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
using Owlsure.UI.WpfUsageDaily.ViewModels;

namespace Owlsure.UI.WpfUsageDaily.Views
{
    /// <summary>
    /// Interaction logic for UsageDailyView.xaml
    /// </summary>
    public partial class UsageDailyView : UserControl
    {
        public UsageDailyView(UsageDailyViewModel viewModel)
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
