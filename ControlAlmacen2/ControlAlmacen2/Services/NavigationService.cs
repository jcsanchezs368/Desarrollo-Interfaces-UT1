using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using ControlAlmacen2.ViewModels;

namespace ControlAlmacen2.Services;

public partial class NavigationService : ObservableObject
{
    [ObservableProperty]
    public ViewModelBase _vistaActual;
    public AvaloniaList<ViewModelBase> ListaVistas { get; } = new();

    public void Navegar(string titulo)
    {
        foreach (var vista in ListaVistas)
        {
            if (vista.Titulo == titulo)
            {
                VistaActual = vista;
                return;
            }
        }
    }
}