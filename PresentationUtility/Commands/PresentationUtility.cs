using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.Prism.Commands;

namespace Owlsure.PresentationUtility.Commands
{
    public class SelectorSelectionChangedCommandBehavior : CommandBehaviorBase<Selector>
    {
        public SelectorSelectionChangedCommandBehavior(Selector selector)
            : base(selector)
        {
            selector.SelectionChanged += OnSelectionChanged;
        }

        void OnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ExecuteCommand();
        }
    }

    public static class SelectorSelect
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(SelectorSelect),
                new PropertyMetadata(OnSetCommandCallback));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(SelectorSelect),
                new PropertyMetadata(OnSetCommandParameterCallback));

        private static readonly DependencyProperty SelectCommandBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "SelectCommandBehavior",
                typeof(SelectorSelectionChangedCommandBehavior),
                typeof(SelectorSelect),
                null);

        private static void OnSetCommandCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            Selector selector = dependencyObject as Selector;

            if (selector != null)
            {
                SelectorSelectionChangedCommandBehavior behavior = GetOrCreateBehavior(selector); ;
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            Selector selector = dependencyObject as Selector;

            if (selector != null)
            {
                SelectorSelectionChangedCommandBehavior behavior = GetOrCreateBehavior(selector);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static SelectorSelectionChangedCommandBehavior GetOrCreateBehavior(Selector selector)
        {
            SelectorSelectionChangedCommandBehavior behavior =
                selector.GetValue(SelectCommandBehaviorProperty) as SelectorSelectionChangedCommandBehavior;

            if (behavior == null)
            {
                behavior = new SelectorSelectionChangedCommandBehavior(selector);
                selector.SetValue(SelectCommandBehaviorProperty, behavior);
            }
            return behavior;

        }

        public static ICommand GetCommand(Selector selector)
        {
            return (selector.GetValue(CommandProperty) as ICommand);
        }
        public static void SetCommand(Selector selector, ICommand command)
        {
            selector.SetValue(CommandProperty, command);
        }

        public static object GetCommandParameter(Selector selector)
        {
            return (selector.GetValue(CommandParameterProperty));
        }
        public static void SetCommandParameter(Selector selector, object parameter)
        {
            selector.SetValue(CommandParameterProperty, parameter);
        }
    }
}
