using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab3Wpf.Models;

public class DataItem : INotifyPropertyChanged, IDataErrorInfo
{
    private string _name = string.Empty;
    private DateTime _date;
    private int _n;
    private double _leftBound;
    private double _rightBound;
    private List<double> _values = new();

    private Func<double, double> F { get; }

    public string Name
    {
        get => _name;
        set
        {
            if (_name == value) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public DateTime Date
    {
        get => _date;
        set
        {
            if (_date == value) return;
            _date = value;
            OnPropertyChanged();
        }
    }

    public int N
    {
        get => _n;
        set
        {
            if (_n == value) return;
            _n = value;
            OnPropertyChanged();
            RecalculateValues();
        }
    }

    public double LeftBound
    {
        get => _leftBound;
        set
        {
            if (Math.Abs(_leftBound - value) < double.Epsilon) return;
            _leftBound = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(RightBound));
            RecalculateValues();
        }
    }

    public double RightBound
    {
        get => _rightBound;
        set
        {
            if (Math.Abs(_rightBound - value) < double.Epsilon) return;
            _rightBound = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(LeftBound));
            RecalculateValues();
        }
    }

    public List<double> Values
    {
        get => _values;
        set
        {
            _values = value;
            OnPropertyChanged();
        }
    }

    public DataItem(string name, DateTime date, int n, (double, double) bounds, Func<double, double> f)
    {
        F = f;
        _name = name;
        _date = date;
        _n = n;
        _leftBound = bounds.Item1;
        _rightBound = bounds.Item2;
        RecalculateValues();
    }

    public DataItem()
        : this("sin(x)", DateTime.Now, 20, (0.0, Math.PI), Math.Sin)
    {
    }

    public void UpdateElement()
    {
        Name = $"{Name}_upd";
        Date = DateTime.Now;
        N = N + 1;
        LeftBound -= 0.25;
        RightBound += 0.25;
    }

    private void RecalculateValues()
    {
        var newValues = new List<double>();

        if (N <= 0 || LeftBound >= RightBound)
        {
            Values = newValues;
            return;
        }

        // В задании явно задан шаг h = (RightBound - LeftBound) / N.
        // Поэтому строим узлы x_0, ..., x_N, включая обе границы отрезка.
        double h = (RightBound - LeftBound) / N;

        for (int i = 0; i <= N; i++)
        {
            double x = LeftBound + i * h;
            newValues.Add(F(x));
        }

        Values = newValues;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Name = {Name}");
        sb.AppendLine($"Date = {Date:dd.MM.yyyy HH:mm:ss}");
        sb.AppendLine($"N = {N}");
        sb.AppendLine($"LeftBound = {LeftBound.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"RightBound = {RightBound.ToString(CultureInfo.InvariantCulture)}");
        sb.Append("Values = [");

        for (int i = 0; i < Values.Count; i++)
        {
            if (i > 0) sb.Append(", ");
            sb.Append(Values[i].ToString("G6", CultureInfo.InvariantCulture));
        }

        sb.AppendLine("]");
        return sb.ToString();
    }

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                nameof(Name) when string.IsNullOrWhiteSpace(Name) =>
                    "Name должен содержать хотя бы один символ.",

                nameof(LeftBound) when LeftBound >= RightBound =>
                    "LeftBound должен быть меньше RightBound.",

                nameof(RightBound) when LeftBound >= RightBound =>
                    "RightBound должен быть больше LeftBound.",

                _ => string.Empty
            };
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
