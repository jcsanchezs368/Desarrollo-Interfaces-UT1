using Almacen.Models;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Almacen.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // public AvaloniaList<string> ListaCategorías { get; set; } = new AvaloniaList<string>();
    // Método corto:
    // public AvaloniaList<string> ListaCategorías { get; set; } = new();
    // Método más corto para las listas:
    public AvaloniaList<string> ListaCategorías { get; set; } = ["Periférico", "RAM", "CPU"];
    [ObservableProperty]
    private int _selectedTab = 0;

    public Componente Componente { get; set; } = new();
    
    public MainViewModel()
    {
        /*ListaCategorías.Add("Peroférico");
        ListaCategorías.Add("RAM");
        ListaCategorías.Add("CPU");
        ListaCategorías.Add("Peroférico");*/
    }
    [RelayCommand]
    public void CambiarTab(string numero)
    {
        int n = int.Parse(numero);
        
        
        if ((SelectedTab + n) >= 0 && (SelectedTab + n) <= 2)
        {
            SelectedTab += n;
        }
        
    }
}