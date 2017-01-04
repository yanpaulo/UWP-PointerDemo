﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace PointerDemo.Converters
{

    public class BoolToColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = value as bool?;
            if (b.HasValue)
            {
                return new SolidColorBrush(b.Value ? Colors.Blue : Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Gray);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
