using System;
using System.Threading.Tasks;
using Almacen.Models;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Almacen.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // public AvaloniaList<string> ListaCategorías { get; set; } = new AvaloniaList<string>();
    // Método corto:
    // public AvaloniaList<string> ListaCategorías { get; set; } = new();
    // Método más corto para las listas:
    public AvaloniaList<string> ListaCategorías { get; set; } = ["Periférico", "RAM", "CPU"];
    [ObservableProperty] private int _selectedTab = 0;

    [ObservableProperty] private Componente _componente = new();


    [ObservableProperty] private bool _atrasVisible = false;
    [ObservableProperty] private bool _finalizarVisible = false;
    [ObservableProperty] private bool _siguienteVisible = true;

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

        ActualizarBotones();

    }

    [RelayCommand]
    public async Task Finalizar()
    {
        bool resultado = await MensajeFinalizar();
        if (!resultado)
        {
            await MostrarMensaje("Proceso cancelado");
        }
    }

    [RelayCommand]
    public void ActualizarBotones()
    {
        if (SelectedTab == 0)
        {
            AtrasVisible = false;
        }
        else
        {
            AtrasVisible = true;
        }

        if (SelectedTab < 2)
        {
            FinalizarVisible = false;
            SiguienteVisible = true;
        }
        else
        {
            FinalizarVisible = true;
            SiguienteVisible = false;
        }

    }

    public void ControladorTab1()
    {
        string mensaje_error = "";
        if (Componente.Categoria == String.Empty)
        {
            mensaje_error += "La categoría es obligatoria\n";
        }
        if (Componente.Referencia == String.Empty)
        {
            mensaje_error += "La referencia es obligatoria\n";
        }
        if (Componente.Nombre == String.Empty)
        {
            mensaje_error += "El nombre es obligatorio\n";
        }
        if (Componente.Descripcion == String.Empty)
        {
            mensaje_error += "La descripción es obligatorio\n";
        }
        
    }
    
    private async Task MostrarMensaje(string mensaje)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Aviso", mensaje, ButtonEnum.Ok);
        await box.ShowAsync();
    }

    private async Task<bool> MensajeFinalizar()
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Confirmar", "¿Quieres finalizar?", ButtonEnum.YesNo);
        await box.ShowAsync();

        var resultado = await box.ShowAsync();
        if (resultado == ButtonResult.Yes)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}