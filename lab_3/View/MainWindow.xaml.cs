using lab_3.View;
using lab_3.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace lab_3
{

    public class RoundValueConvertor : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            return Math.Round((double)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse((string)value);
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Свойства
        public static readonly DependencyProperty PointsPositiveProperty = DependencyProperty.Register(
            "PointsPositive", typeof(ObservableCollection<Point>), typeof(MainWindow), new PropertyMetadata(default(ObservableCollection<Point>)));

        public ObservableCollection<Point> PointsPositive
        {
            get
            {
                return (ObservableCollection<Point>)GetValue(PointsPositiveProperty);
            }
            set
            {
                SetValue(PointsPositiveProperty, value);
            }
        }

        public static readonly DependencyProperty PointsNegativeProperty = DependencyProperty.Register(
            "PointsNegative", typeof(ObservableCollection<Point>), typeof(MainWindow), new PropertyMetadata(default(ObservableCollection<Point>)));

        public ObservableCollection<Point> PointsNegative
        {
            get
            {
                return (ObservableCollection<Point>)GetValue(PointsNegativeProperty);
            }
            set
            {
                SetValue(PointsNegativeProperty, value);
            }
        }

        public static readonly DependencyProperty AProperty = DependencyProperty.Register(
            "A", typeof(double), typeof(MainWindow), new PropertyMetadata(5.0));

        public double A
        {
            get
            {
                return (double)GetValue(AProperty);
            }
            set
            {
                SetValue(AProperty, value);
            }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.AboutShow)
                AboutProgram();
        }

        private void SettingsVisibilityHidden(object sender, RoutedEventArgs e)
        {
            g_settings.Visibility = Visibility.Hidden;
        }

        private void SettingsVisibilityVisibility(object sender, RoutedEventArgs e)
        {
            g_settings.Visibility = Visibility.Visible;
        }

        private void AboutProgram()
        {
            var result = MessageBox.Show(MainVM.AboutMessage() + Environment.NewLine + "Показывать это окно в дальнейшем?", "О программе", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Properties.Settings.Default.AboutShow = result == System.Windows.Forms.DialogResult.Yes;
            Properties.Settings.Default.Save();
        }

        private void AboutProgram(object sender, RoutedEventArgs e)
        {
            AboutProgram();
        }

        private void WriteSettingsValue(object sender, RoutedEventArgs e)
        {
            string file_path = string.Empty;
            string file_name = string.Empty;
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(file_path = saveFileDialog1.FileName))
                    {
                        file_name = Path.GetFileName(file_path);
                        if(MainVM.WriteFile(new double[] {db_size.Value.Value, db_left_border.Value.Value, db_right_border.Value.Value, db_step.Value.Value}, file_path))
                            MessageBox.Show($"Файл \"{file_name}\" успешно записан", file_name);
                        else
                            MessageBox.Show($"Файл \"{file_name}\" не удалось записать", file_name);
                    }
                }
            }
        }

        private void SettingsValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {           
            if (db_left_border == null || db_right_border == null)
                return;
            List<Point> points_p = new List<Point>();
            List<Point> points_n = new List<Point>();
            double _a = Math.Abs(A / 0.3);
            double left_border = -_a;
            double right_border = _a;
            if (db_left_border.Value.HasValue && db_right_border.Value.HasValue)
            {
                left_border = db_left_border.Value.Value / 0.3;
                right_border = db_right_border.Value.Value / 0.3;
                if (left_border < -_a)
                    left_border = -_a;
                if (right_border > _a)
                    right_border = _a;
                if (right_border < left_border)
                {
                    if(((System.Windows.Controls.Slider)sender) == sl_left_border)
                        db_right_border.Value = db_left_border.Value;
                    else
                        db_left_border.Value = db_right_border.Value;
                }                    
            }

            for (double x = left_border; x <= left_border + 0.001; x += 0.0001)
            {
                double y = MainVM.GetFunc(x, _a);
                if (!double.IsNaN(y))
                {
                    points_p.Add(new Point(x, -y));
                    points_n.Add(new Point(x, y));
                }
            }

            for (double x = left_border + 0.01; x < right_border - 0.01; x += 0.1)
            {
                double y = MainVM.GetFunc(x, _a);
                if (!double.IsNaN(y))
                {
                    points_p.Add(new Point(x, -y));
                    points_n.Add(new Point(x, y));
                }
            }

            for (double x = right_border - 0.001; x <= right_border; x += 0.0001)
            {
                double y = MainVM.GetFunc(x, _a);
                if (!double.IsNaN(y))
                {
                    points_p.Add(new Point(x, -y));
                    points_n.Add(new Point(x, y));
                }
            }
            PointsPositive = new ObservableCollection<Point>(points_p);
            PointsNegative = new ObservableCollection<Point>(points_n);
        }

        private void ShowTable(object sender, RoutedEventArgs e)
        {
            double _a = Math.Abs(A);
            double left_border = -_a;
            double right_border = _a;
            if (db_left_border.Value.HasValue && db_right_border.Value.HasValue)
            {
                left_border = db_left_border.Value.Value;
                right_border = db_right_border.Value.Value;
                if (left_border < -_a)
                    left_border = -_a;
                if (right_border > _a)
                    right_border = _a;
            }

            List<TableValue> table = new List<TableValue>();
            double step = db_step.Value.Value;
            for (double x = left_border; x <= right_border; x += step)
            {
                
                TableValue row = new TableValue();
                row.X = Math.Round(x, 2);
                row.Y = Math.Round(MainVM.GetFunc(x, _a), 2);
                table.Add(row);
            }
            TableValueWindow Table = new TableValueWindow(table);
            Table.Show();
        }

        private void ReadSettingsValue(object sender, RoutedEventArgs e)
        {
            string file_path = string.Empty;
            string file_name = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    file_path = openFileDialog.FileName;
                    file_name = Path.GetFileName(file_path);
                }
            }

            if (file_path != string.Empty)
            {
                double[] settings;
                if (MainVM.ReadFile(out settings, file_path))
                {
                    db_size.Value = settings[0];
                    db_left_border.Value = settings[1];
                    db_right_border.Value = settings[2];
                    db_step.Value = settings[3];
                    MessageBox.Show($"Файл \"{file_name}\" прочтен");
                }
                else
                {
                    MessageBox.Show($"Файл \"{file_name}\" не содержит исходный текст");
                }
            }
        }
    }
}
