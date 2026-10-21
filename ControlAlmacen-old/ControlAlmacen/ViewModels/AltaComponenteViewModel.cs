using System;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlAlmacen.Services;
using ControlAlmacen2.Models;
using SukiUI.Toasts;

namespace ControlAlmacen.ViewModels;

public partial class AltaComponenteViewModel : ViewModelBase
{
    private FilePickerService _filePickerService = new();
    private N8NService _n8nService = new();
    private IStorageFile _imagenPicker;
    [ObservableProperty] private bool _mostrarImagen;
    [ObservableProperty] private Bitmap? _imagen;
    [ObservableProperty] AvaloniaList<Proveedor>  _listaProveedores;
    [ObservableProperty] AvaloniaList<Componente>  _listaComponentes;
    
    [RelayCommand]
    public async Task ObtenerImagenes()
    {
        await _n8nService.ObtenerImagenes(Guid.Parse("2f8ba41e-de1e-4f30-84b8-fd9e3d8b5d6e"));
    }
    
    
    [RelayCommand]
    public async Task SeleccionarImagen()
    {
        _imagenPicker = await _filePickerService.SeleccionaImagen();
        if (_imagenPicker == null)
        {
            return;
        }

        MostrarImagen = true;
        await using var stream = await _imagenPicker.OpenReadAsync();
        Imagen = new Bitmap(stream);
        // var uidproducto = Guid.Parse("2f8ba41e-de1e-4f30-84b8-fd9e3d8b5d6e");
        // _n8nService.EnviarImagen(imagenPicker,uidproducto);


    }

    [RelayCommand]
    public async Task CrearProducto()
    {
        var producto = new Componente
        {
            Nombre = "Memoria RAM",
            Codigo = "RAM2",
            Descripcion = "Memoria muy cara",
            PrecioVenta = 200,
            Stock = 5,
            Activo = true,
            FechaAlta = DateTime.Now,
        };
        
        Guid uuid = await _n8nService.EnviarComponente(producto);
        
        Console.WriteLine("Producto creado con UUID: " + uuid);

        await _n8nService.EnviarImagen(_imagenPicker, uuid);
    }

    [RelayCommand]
    public async Task ObtenerProveedores()
    {
        ListaProveedores = await _n8nService.ObtenerProveedores();
    }

    [RelayCommand]
    public async Task ObtenerComponentes()
    {
        ListaComponentes = await _n8nService.ObtenerComponentes();
    }
}