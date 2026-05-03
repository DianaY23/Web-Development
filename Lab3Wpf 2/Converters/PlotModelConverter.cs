using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Lab3Wpf.Converters;

public class PlotModelConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var model = new PlotModel { Title = "Значения функции на равномерной сетке" };
        model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "x" });
        model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "F(x)" });

        if (values.Length < 4 ||
            values[0] == DependencyProperty.UnsetValue ||
            values[1] == DependencyProperty.UnsetValue ||
            values[2] == DependencyProperty.UnsetValue ||
            values[3] == DependencyProperty.UnsetValue)
        {
            model.Title = "Выберите строку в DataGrid";
            return model;
        }

        if (values[0] is not double left ||
            values[1] is not double right ||
            values[2] is not int n ||
            values[3] is not List<double> functionValues ||
            n <= 0 ||
            functionValues.Count == 0)
        {
            model.Title = "Нет данных для построения графика";
            return model;
        }

        double h = (right - left) / n;
        var series = new LineSeries
        {
            Title = "F(x)",
            MarkerType = MarkerType.Circle,
            MarkerSize = 3
        };

        for (int i = 0; i < functionValues.Count; i++)
        {
            double x = left + i * h;
            series.Points.Add(new DataPoint(x, functionValues[i]));
        }

        model.Series.Add(series);
        return model;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
