using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Lab3Wpf.Converters;

public class GridValuesConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 4 ||
            values[0] == DependencyProperty.UnsetValue ||
            values[1] == DependencyProperty.UnsetValue ||
            values[2] == DependencyProperty.UnsetValue ||
            values[3] == DependencyProperty.UnsetValue)
        {
            return Array.Empty<string>();
        }

        if (values[0] is not double left ||
            values[1] is not double right ||
            values[2] is not int n ||
            values[3] is not List<double> functionValues ||
            n <= 0)
        {
            return Array.Empty<string>();
        }

        double h = (right - left) / n;
        var result = new List<string>();

        for (int i = 0; i < functionValues.Count; i++)
        {
            double x = left + i * h;
            result.Add($"i = {i,2};  x = {x,12:G6};  f(x) = {functionValues[i],12:G6}");
        }

        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
