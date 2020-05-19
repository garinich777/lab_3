using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SettingsVisibilityHidden(object sender, RoutedEventArgs e)
        {
            g_settings.Visibility = Visibility.Hidden;
        }

        private void SettingsVisibilityVisibility(object sender, RoutedEventArgs e)
        {
            g_settings.Visibility = Visibility.Visible;
        }
    }
}
