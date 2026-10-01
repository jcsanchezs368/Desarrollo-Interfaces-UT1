using System;
using System.Threading.Tasks;
using Almacen.Models;
using Almacen.Services;
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
    [ObservableProperty] private string _mensaje = "";

    private N8NService n8NService { get; set; } = new();

    [ObservableProperty] private AvaloniaList<Componente> _componentes;
    
    [RelayCommand]
    public async Task ObtenerComponentes()
    {
        Componentes = await n8NService.ObtenerComponentes();
    }
    public MainViewModel()
    {
        /*ListaCategorías.Add("Peroférico");
        ListaCategorías.Add("RAM");
        ListaCategorías.Add("CPU");
        ListaCategorías.Add("Peroférico");*/
    }

    partial void OnSelectedTabChanged(int value)
    {
        Console.Write("OnSelectedTabChanged " + value);
        if (value == 3)
        {
            ObtenerComponentes();
        }
        
        ActualizarBotones();
    }

    private bool ValidarMensaje()
    {
        Mensaje = "";
        if (Componente.Categoria.Trim().Equals(String.Empty))
        {
            Mensaje += "La categoría es obligatoria\n";

        }
        if (Componente.Referencia.Trim().Equals(String.Empty))
        {
            Mensaje += "La referencia es obligatoria\n";

        }

        if (Componente.Nombre.Trim().Equals(String.Empty)){
            Mensaje += "El nombre es obligatorio\n";

        }
        if (Componente.Descripcion.Trim().Equals(String.Empty))
        {
            Mensaje += "La descripción es obligatorio\n";

        }

        if (Mensaje != String.Empty)
        {
            return false;
        }

        return true;
    }


    [RelayCommand]
    public void CambiarTab(string numero)
    {
        int n = int.Parse(numero);

        
        if ((SelectedTab + n) >= 0 && (SelectedTab + n) <= 2)
        {
            if (n > 0)
            {
                if (ValidarMensaje())
                {
                    SelectedTab += n;
                }
            }
            else
            {
                SelectedTab += n;    
            }
            
        }

    }

    [RelayCommand]
    public async Task Finalizar()
    {
        bool resultado = await MensajeFinalizar();
        if (!resultado)
        {
            await MostrarMensaje("Proceso cancelado");
        }else
        {
            await n8NService.Crear(Componente);
            SelectedTab = 3;
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

        if (SelectedTab == 3)
        {
            FinalizarVisible = false;
            SiguienteVisible = false;
            AtrasVisible = true;
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
        var resultado = await box.ShowAsync();
        if (resultado == ButtonResult.Yes)
        {
            return true;
        }
        return false;
    }

}