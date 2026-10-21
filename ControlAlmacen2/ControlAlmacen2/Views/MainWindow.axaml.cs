using Avalonia.Controls;
using ControlAlmacen2.ViewModels;

namespace ControlAlmacen2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}