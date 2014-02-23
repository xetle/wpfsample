using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using Owlsure.UI.WpfUsage.ViewModels;
using System.Windows.Data;

namespace Owlsure.UI.WpfUsage.Extensions
{
    public class DataGridExtension
    {
        public static readonly DependencyProperty MatrixSourceProperty =
            DependencyProperty.RegisterAttached("MatrixSource",
            typeof(PivotedUsageData), typeof(DataGridExtension),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnMatrixSourceChanged)));

        public static PivotedUsageData GetMatrixSource(DependencyObject d)
        {
            return (PivotedUsageData)d.GetValue(MatrixSourceProperty);
        }

        public static void SetMatrixSource(DependencyObject d, PivotedUsageData value)
        {
            d.SetValue(MatrixSourceProperty, value);
        }

        private static void OnMatrixSourceChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = d as DataGrid;
            if (d == null)
            {
                return;
            }

            PivotedUsageData pivotedUsageData = e.NewValue as PivotedUsageData;

            dataGrid.Columns.Clear();

            dataGrid.ItemsSource = pivotedUsageData.UsageRows;

            var bindingDate = new Binding(string.Format("[0].ExposureDate"));
            bindingDate.StringFormat = "yyyy-MM-dd";
            dataGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Exposure Date",
                    Binding = bindingDate,
                    CellStyle = (Style)dataGrid.FindResource("DateCellStyle")
                });

            int idxCol = 0;
            foreach (var c in pivotedUsageData.ColumnNames)
            {
                var binding = new Binding(string.Format("[{0}].Exposure", idxCol));
                binding.StringFormat = "{0:n0}";
                dataGrid.Columns.Add(
                    new DataGridTextColumn()
                    {
                        Header = c,
                        Binding = binding,
                        CellStyle = (Style)dataGrid.FindResource("NumberCellStyle")
                    });
                idxCol++;
            }
        }
    }
}
