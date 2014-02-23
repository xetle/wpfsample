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
using Microsoft.Practices.Prism.Regions;

namespace Owlsure.UI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel, IRegionManager regionManager)
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = viewModel;

                //regionManager.Regions[Globals.RegionNames.RightRegion].NavigationService.Navigating += new EventHandler<RegionNavigationEventArgs>(NavigationService_Navigating);
                //regionManager.Regions[Globals.RegionNames.RightRegion].NavigationService.Navigated += new EventHandler<RegionNavigationEventArgs>(NavigationService_Navigated);
                //regionManager.Regions[Globals.RegionNames.RightRegion].NavigationService.NavigationFailed += new EventHandler<RegionNavigationFailedEventArgs>(NavigationService_NavigationFailed);
            };
        }

        void NavigationService_NavigationFailed(object sender, RegionNavigationFailedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void NavigationService_Navigated(object sender, RegionNavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void NavigationService_Navigating(object sender, RegionNavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }


    }
}
