﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UrlToolkit.Common.Converters
{
    class ContentLengthToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is int && (int)value != 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
