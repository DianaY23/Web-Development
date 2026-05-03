# Лабораторная работа 3 — WPF, DataGrid, ListBox, OxyPlot

## Что реализовано

- `DataItem` реализует `INotifyPropertyChanged` и `IDataErrorInfo`.
- `DataCollection` наследуется от `ObservableCollection<DataItem>`.
- В `DataGrid` выводятся только `Name`, `Date`, `N`, `LeftBound`, `RightBound`.
- Для `Name`, `LeftBound`, `RightBound` подключена проверка корректности через `IDataErrorInfo`.
- При изменении `N`, `LeftBound`, `RightBound` автоматически пересчитывается список `Values`.
- `ListBox` заполняется через `Binding` + `IMultiValueConverter`, без обработки `SelectionChanged`.
- График строится через `OxyPlot.Wpf.PlotView` и converter.
- Кнопки:
  - `Update` вызывает `DataCollection.UpdateCollection()`;
  - `Update Element` вызывает `DataItem.UpdateElement()` для выбранной строки;
  - `Output` выводит содержимое `DataCollection`.

## Как запустить

1. Открыть `Lab3Wpf.csproj` в Visual Studio 2022.
2. Дождаться восстановления NuGet-пакетов.
3. Запустить проект.

Проект использует `net8.0-windows` и пакет `OxyPlot.Wpf`.
