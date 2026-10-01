using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControlAlmacen.ViewModels;

namespace ControlAlmacen.Views;

public partial class AltaComponenteView : UserControl
{
    public AltaComponenteView()
    {
        InitializeComponent();
        DataContext = new AltaComponenteViewModel();
    }
}