using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;

namespace Owlsure.UI.Wpf.PopupBehavior
{
    public static class RegionPopupBehavior
    {
        public static readonly DependencyProperty CreatePopupRegionWithNameProperty =
            DependencyProperty.RegisterAttached("CreatePopupRegionWithName", typeof(string), typeof(RegionPopupBehavior),
            new PropertyMetadata(CreatePopupRegionWithNamePropertyChanged)
            );

        public static string GetCreatePopupRegionWithName(DependencyObject owner)
        {
            return owner.GetValue(CreatePopupRegionWithNameProperty) as string;
        }

        public static void SetCreatePopupRegionWithName(DependencyObject owner, string value)
        {
            owner.SetValue(CreatePopupRegionWithNameProperty, value);
        }

        static void CreatePopupRegionWithNamePropertyChanged(DependencyObject hostControl, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(hostControl))
            {
                IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                if (regionManager != null)
                {
                    IRegion region = new SingleActiveRegion();
                    MyRegionBehavior behavior = new MyRegionBehavior();
                    behavior.HostControl = hostControl;
                    region.Behaviors.Add("PopupRegionBehavior", behavior);
                    regionManager.Regions.Add(e.NewValue as string, region);
                }
            }
        }
    }

    public class MyRegionBehavior : RegionBehavior
    {
        // What object have we attached the CreatePopupRegionWithNameProperty to. 
        // The pop up window will have this as it's owner
        public DependencyObject HostControl { get; set; }

        protected override void OnAttach()
        {
            Debug.WriteLine(Region.ActiveViews.Count());

            this.Region.ActiveViews.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ActiveViews_CollectionChanged);
        }

        void ActiveViews_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine(Region.ActiveViews.Count());

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                // So if the region's active view have changed we need to create a new window to host the view
                Window window = new Window();
                // and set the content to be the new view
                window.Content = e.NewItems[0];
                window.Owner = HostControl as Window;
                window.Title = e.NewItems[0].ToString();
                window.SizeToContent = SizeToContent.WidthAndHeight;
                window.Show(); // could ShowDialog
            }
        }
    }
}
