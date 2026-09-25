using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Almacen.Models;

namespace Almacen.Services;

public class N8NService
{
    private HttpClient cliente = new();
    private string url = "http://192.168.29.12:11086/webhook-test";
    
    public async Task Crear(Componente componente)
    {
        await cliente.PostAsJsonAsync(url + "/crear", componente);
    }
}