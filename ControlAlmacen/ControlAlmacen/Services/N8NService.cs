using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ControlAlmacen.Models;
using Avalonia.Collections;

namespace Almacen.Services;

public class N8NService
{
    private HttpClient cliente = new();
    private string url = "http://192.168.29.12:11086/webhook";
    
    public async Task Crear(Componente componente)
    {
        await cliente.PostAsJsonAsync(url + "/crearComponente", componente);
    }

    public async Task<AvaloniaList<Componente>> ObtenerComponentes()
    {
        return await cliente.GetFromJsonAsync<AvaloniaList<Componente>>(url + "/componentes");
        
    }
}