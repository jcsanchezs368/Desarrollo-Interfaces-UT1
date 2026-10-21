using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using ControlAlmacen2.Services;

namespace ControlAlmacen2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public NavigationService NavigationService { get; } = new();
    public MainViewModel()
    {
        NavigationService.ListaVistas.Add(new ConsultasViewModel(NavigationService));
        NavigationService.ListaVistas.Add(new AltaComponenteViewModel(NavigationService));
        NavigationService.Navegar("Consultas");
        
    }
    
}