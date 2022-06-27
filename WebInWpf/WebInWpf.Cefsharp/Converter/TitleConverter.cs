﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WebInWpf.Cefsharp.Converter
{
    public class TitleConverter : IValueConverter
    {
#if DEBUG
        private const string Build = "Debug";
#else
        private const string Build = "Release";
#endif
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "WebInWpf.Cefsharp (" + Build + ") - " + (value ?? "No Title Specified");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
