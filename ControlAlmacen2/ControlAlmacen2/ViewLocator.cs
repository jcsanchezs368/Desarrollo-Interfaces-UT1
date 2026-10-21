using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ControlAlmacen2.ViewModels;

namespace ControlAlmacen2;

[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var viewModelName = param.GetType().FullName!;

        var viewName = viewModelName
            .Replace(".ViewModels.", ".Views.", StringComparison.Ordinal)
            .Replace("ViewModel", "View", StringComparison.Ordinal);

        var type = Type.GetType(viewName);

        if (type is not null)
            return (Control)Activator.CreateInstance(type)!;

        return new TextBlock
        {
            Text = $"No se ha encontrado la vista: {viewName}"
        };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }

}