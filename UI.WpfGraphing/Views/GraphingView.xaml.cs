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
using Owlsure.UI.WpfGraphing.ViewModels;

namespace Owlsure.UI.WpfGraphing.Views
{
    /// <summary>
    /// Interaction logic for GraphingView.xaml
    /// </summary>
    public partial class GraphingView : UserControl
    {
        public GraphingView(GraphingViewModel viewModel)
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;
            };
        }
    }
}
