using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lab3Wpf.Models;

namespace Lab3Wpf.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private DataItem? _selectedItem;
    private string _output = string.Empty;

    public DataCollection Items { get; } = new();

    public DataItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (ReferenceEquals(_selectedItem, value)) return;
            _selectedItem = value;
            OnPropertyChanged();
        }
    }

    public string Output
    {
        get => _output;
        set
        {
            if (_output == value) return;
            _output = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
