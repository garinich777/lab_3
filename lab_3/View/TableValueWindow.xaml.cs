using System.Collections.Generic;
using System.Windows;

namespace lab_3.View
{
    /// <summary>
    /// Логика взаимодействия для TableValue.xaml
    /// </summary>
    public partial class TableValueWindow : Window
    {
        public TableValueWindow(List<TableValue> table)
        {
            InitializeComponent();
            dg_table.ItemsSource = table;
        }
    }
}
