using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Lab3Wpf.Models;

public class DataCollection : ObservableCollection<DataItem>
{
    private int _updateNumber;

    public DataCollection()
    {
        AddDefaultItems();
    }

    public void UpdateCollection()
    {
        _updateNumber++;
        Clear();

        Add(new DataItem(
            $"sin(x) #{_updateNumber}",
            DateTime.Now,
            12 + _updateNumber,
            (0.0, Math.PI + _updateNumber * 0.1),
            Math.Sin));

        Add(new DataItem(
            $"cos(x) #{_updateNumber}",
            DateTime.Now,
            16 + _updateNumber,
            (-Math.PI / 2, Math.PI / 2 + _updateNumber * 0.1),
            Math.Cos));

        Add(new DataItem(
            $"x^2 #{_updateNumber}",
            DateTime.Now,
            10 + _updateNumber,
            (-2.0, 2.0 + _updateNumber * 0.1),
            x => x * x));
    }

    private void AddDefaultItems()
    {
        Add(new DataItem("sin(x)", DateTime.Now, 20, (0.0, 2 * Math.PI), Math.Sin));
        Add(new DataItem("cos(x)", DateTime.Now, 20, (0.0, 2 * Math.PI), Math.Cos));
        Add(new DataItem("x^2", DateTime.Now, 16, (-2.0, 2.0), x => x * x));
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("DataCollection");
        sb.AppendLine(new string('-', 60));

        for (int i = 0; i < Count; i++)
        {
            sb.AppendLine($"Element {i}");
            sb.AppendLine(this[i].ToString());
            sb.AppendLine(new string('-', 60));
        }

        return sb.ToString();
    }
}
