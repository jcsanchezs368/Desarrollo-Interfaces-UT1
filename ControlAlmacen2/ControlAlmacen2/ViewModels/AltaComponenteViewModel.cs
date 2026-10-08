using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlAlmacen2.Services;

namespace ControlAlmacen2.ViewModels;

public partial class AltaComponenteViewModel : ViewModelBase
{
    private N8NService _n8nService = new();
    private FilePickerService _filePickerService = new();
    [ObservableProperty] private bool _mostrarImagen;
    [ObservableProperty] private Bitmap? _imagen;

    [RelayCommand]
    public async Task SeleccionarImagen()
    {
        var imagenPicker = await _filePickerService.SeleccionaImagen();
        if (imagenPicker == null)
        {
            return;
        }

        MostrarImagen = true;
        await using var stream = await imagenPicker.OpenReadAsync();
        Imagen = new Bitmap(stream);

        _n8nService.EnviarImagen(imagenPicker);

    }
    [RelayCommand]
    public async Task CrearProducto()
    {
        
    }
    
}