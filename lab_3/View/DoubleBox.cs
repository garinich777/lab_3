using System;
using System.Windows;
using System.Windows.Controls;

namespace lab_3.View
{
    class DoubleBox : TextBox
    {
        private double _value;
        private double _max_value;
        private double _min_value;
        private double _num_simbols_after_com;

        public event RoutedEventHandler ErrorSymbol
        {
            add { AddHandler(ErrorSymbolEvent, value); }
            remove { RemoveHandler(ErrorSymbolEvent, value); }
        }
        public static readonly RoutedEvent ErrorSymbolEvent =
            EventManager.RegisterRoutedEvent("ErrorSymbol", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DoubleBox));

        void RaiseErrorSymbolEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ErrorSymbolEvent);
            RaiseEvent(newEventArgs);
        }

        public double? Value
        {
            get { return (double?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double?), typeof(DoubleBox), new PropertyMetadata(null));

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(DoubleBox), new PropertyMetadata(10000.0));

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(DoubleBox), new PropertyMetadata(-10000.0));

        public DoubleBox()
        {
            TextChanged += tbNumBox_TextChanged;
            _min_value = MinValue;
            _max_value = MaxValue;
            _value = 0;
        }

        private void RemoveSymbol()
        {
            Text = Text.Remove(Text.Length - 1);
            SelectionStart = Text.Length;
            RaiseErrorSymbolEvent();
        }

        private void tbNumBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _min_value = MinValue;
            _max_value = MaxValue;
            string _text = Text;
            double el = 0;
            _text = _text.Replace(".", ",");
            string[] temp = _text.Split(',');
            if (temp.Length == 2)
                while (temp[1].Length > 2)
                {
                    _text = _text.Remove(_text.Length - 1);
                    temp = _text.Split(',');
                }

            if (_text != "")
            {
                if (!double.TryParse(_text, out el))
                {
                    if (_text.Remove(0, Text.Length - 1) != "," || _text.Remove(1, Text.Length - 1) != "-")
                        RemoveSymbol();
                }
                else
                {
                    _value = el;
                    if (_value < _min_value || _value > _max_value) RemoveSymbol();
                    if (_text == "-" && _min_value >= 0) RemoveSymbol();
                    Value = _value;
                }
            }
            else
                Value = null;            
        }
    }
}
