using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Almacen.Models;

public partial class Componente:ObservableObject
{
    [ObservableProperty] private string _categoria = "";
    [ObservableProperty] private string _referencia = "";
    [ObservableProperty] private string _nombre = "";
    [ObservableProperty] private string _descripcion = string.Empty;
    [ObservableProperty] private decimal _precio = 0;
    [ObservableProperty] private int _cantidad = 0;
    [ObservableProperty] private bool _disponible = false;
    [ObservableProperty] private DateTime _fecha  = DateTime.Now;
}