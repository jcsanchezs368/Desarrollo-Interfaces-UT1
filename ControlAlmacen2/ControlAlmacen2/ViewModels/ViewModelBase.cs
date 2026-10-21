using CommunityToolkit.Mvvm.ComponentModel;
using SukiUI.Dialogs;

namespace ControlAlmacen2.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string _titulo = string.Empty;
    [ObservableProperty]
    private string _icono =  string.Empty;
}