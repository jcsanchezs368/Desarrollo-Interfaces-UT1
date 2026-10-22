using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Xaml.Interactions.Custom;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlAlmacen2.Models;
using ControlAlmacen2.Services;
using ControlAlmacen2.Views.Dialogs;
using DialogHostAvalonia;
using SukiUI.Dialogs;

namespace ControlAlmacen2.ViewModels;

public partial class ConsultasViewModel : ViewModelBase
{
    
    private N8NService _n8nService = new();
    private readonly NavigationService _navigationService;
    [ObservableProperty] AvaloniaList<Proveedor>  _listaProveedores;
    [ObservableProperty] AvaloniaList<Componente>  _listaComponentes;
    [ObservableProperty] private Componente _selectedComponente;
    
    public ConsultasViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        Titulo = "Consultas";
        Icono = "Search";
    }


    [RelayCommand]
    public void AbrirComponenteDialogo()
    {
        DialogHost.Show(new ComponenteDialog(), "ComponenteDialog");
    }
    
    [RelayCommand]
    public void IrAAltaComponente()
    {
        _navigationService.Navegar("Crear Componente");
    }
    [RelayCommand]
    public async Task ObtenerComponentes()
    {
        ListaComponentes = await _n8nService.ObtenerComponentes();
    }

    [RelayCommand]
    public async Task EditarComponente()
    {
        await _n8nService.EditarComponente(SelectedComponente);
    }
}