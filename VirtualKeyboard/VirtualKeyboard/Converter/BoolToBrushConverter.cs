using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Converter
{
    public class BoolToBrushConverter : IValueConverter
    {
       
        public Color? TrueColor { get; set; }

        public Color? FalseColor { get; set; }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not bool b)
            {
                throw new InvalidCastException();
            }

            return b ? new SolidColorBrush() { Color = TrueColor } : new SolidColorBrush() { Color = FalseColor };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
