using System.Linq;
using System.Windows;
using Lab3Wpf.ViewModels;

namespace Lab3Wpf;

public partial class MainWindow : Window
{
    private MainViewModel ViewModel => (MainViewModel)DataContext;

    public MainWindow()
    {
        InitializeComponent();

        var viewModel = new MainViewModel();
        viewModel.SelectedItem = viewModel.Items.FirstOrDefault();
        DataContext = viewModel;
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Items.UpdateCollection();
        ViewModel.SelectedItem = ViewModel.Items.FirstOrDefault();
        ViewModel.Output = "Коллекция обновлена.";
    }

    private void UpdateElement_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedItem is null)
        {
            MessageBox.Show("Сначала выберите элемент в DataGrid.", "Нет выбранного элемента");
            return;
        }

        ViewModel.SelectedItem.UpdateElement();
        ViewModel.Output = "Выбранный элемент обновлен.";
    }

    private void Output_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Output = ViewModel.Items.ToString();
    }
}
