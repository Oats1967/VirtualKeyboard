using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Controls;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Converter
{
    public class LayoutToKeyboardConverter : IValueConverter
    {
        private readonly IServiceProvider _services;

        public LayoutToKeyboardConverter(IServiceProvider services)
        {
            _services = services;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                // Add new Layout here
                Layouts.Numeric => _services.GetService<NumericKeyboard>(),
                Layouts.German => _services.GetService<GermanKeyboard>(),
                Layouts.English => _services.GetService<EnglishKeyboard>(),
                Layouts.Dutch => _services.GetService<DutchKeyboard>(),
                _ => throw new NotImplementedException($"{value} layout is not implemented.")
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
