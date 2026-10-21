using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using ControlAlmacen.ViewModels;

namespace ControlAlmacen;

public partial class NavigationService : ObservableObject
{
    [ObservableProperty] private ViewModelBase _vistaActual;
    public AvaloniaList<ViewModelBase> _listaVistas = new();

    public void Navegar(string titulo)
    {
        foreach (var vista in _listaVistas)
        {
            if (vista.Titulo == titulo)
            {
                VistaActual = vista;
                return;
            }
        }
    }
}