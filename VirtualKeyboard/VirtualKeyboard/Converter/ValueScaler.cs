using System.Globalization;

namespace VirtualKeyboard.Converter
{
    public class ValueScaler : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue && parameter is string scaleStr &&
                double.TryParse(scaleStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double scale))
            {
                return doubleValue * scale;
            }

            return 0; // fallback font size
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}