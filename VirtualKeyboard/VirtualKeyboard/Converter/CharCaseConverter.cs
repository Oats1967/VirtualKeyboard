using System.Globalization;

namespace VirtualKeyboard.Converter
{
    public class CharCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isShift && parameter is string letter && letter.Length == 1)
            {
                return isShift ? letter.ToUpper() : letter.ToLower();
            }
            if(parameter is null)
            {
                return string.Empty;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
