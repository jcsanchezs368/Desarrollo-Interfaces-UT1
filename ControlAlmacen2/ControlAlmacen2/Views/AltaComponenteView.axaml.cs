using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControlAlmacen2.ViewModels;

namespace ControlAlmacen2.Views;

public partial class AltaComponenteView : UserControl
{
    public AltaComponenteView()
    {
        InitializeComponent();
        DataContext = new AltaComponenteViewModel();
    }
}