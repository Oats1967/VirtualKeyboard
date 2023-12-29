using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Converter
{
    class CapsLockToUpperLowerCaseConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is not bool capsLock || value is not string text)
            {
                throw new InvalidCastException();
            }

            return capsLock ? text.ToUpper() : text.ToLower();
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
