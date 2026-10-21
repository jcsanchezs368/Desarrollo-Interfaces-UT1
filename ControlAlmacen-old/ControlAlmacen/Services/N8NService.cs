using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Platform.Storage;
using ControlAlmacen2.Models;

namespace ControlAlmacen.Services;

public class N8NService
{
    //private HttpClient cliente = new ();
    private string url = "http://192.168.29.12:11086/webhook";

    public async Task<Guid> EnviarComponente(Componente componente)
    {
        using HttpClient cliente = new();
        var respuesta = await cliente.PostAsJsonAsync(url+"/componente", componente);
        respuesta.EnsureSuccessStatusCode();
        var json = await respuesta.Content.ReadFromJsonAsync<JsonElement>();
        string uidproducto = json.GetProperty("id").GetString();
        return Guid.Parse(uidproducto);
    }
    
    public async Task EnviarImagen(IStorageFile imagen, Guid uidproducto)
    {
        using HttpClient cliente = new();
        using var contenido = new MultipartFormDataContent();
        await using var stream = await imagen.OpenReadAsync();
        var archivo  = new StreamContent(stream);
        archivo.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        contenido.Add(archivo, "imagen", imagen.Name);
        await cliente.PostAsync(url+"/imagenes/?uidproducto="+uidproducto, contenido);
    }
    
    public async Task EnviarImagenSola(IStorageFile imagen)
    {
        using HttpClient cliente = new();
        using var contenido = new MultipartFormDataContent();
        await using var stream = await imagen.OpenReadAsync();
        var archivo  = new StreamContent(stream);
        archivo.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        contenido.Add(archivo, "imagen", imagen.Name);
        await cliente.PostAsync(url+"/imagen", contenido);
    }

    public async Task<AvaloniaList<string>> ObtenerImagenes(Guid uidproducto)
    {
        using HttpClient cliente = new();
        var listJson = await cliente.GetFromJsonAsync<JsonElement>(url+"/lista/?uidproducto="+uidproducto);
        var elementos = listJson.EnumerateArray().ToArray();
        AvaloniaList<string> listaUrlImagenes = new();
        foreach (var elemento in elementos)
        {
            string urlImagen =url + "/imagenes?uidproducto="+uidproducto +"&nombre="+  elemento.GetProperty("name").GetString();
            listaUrlImagenes.Add(urlImagen);
        }
        return listaUrlImagenes;
    }

    public async Task<AvaloniaList<Proveedor>> ObtenerProveedores()
    {
        using HttpClient cliente = new();
        var listJson = await cliente.GetFromJsonAsync<JsonElement>(url+"/proveedores");
        var elementos = listJson.EnumerateArray().ToArray();
        AvaloniaList<Proveedor> listaProveedores = new();
        foreach (var elemento in elementos)
        {
            listaProveedores.Add(new Proveedor
            {
                Id = elemento.GetProperty("id").GetGuid(),
                Nombre = elemento.GetProperty("nombre").GetString()
            });
        }
        return listaProveedores;
    }
    
    public async Task<AvaloniaList<Componente>> ObtenerComponentes()
    {
        using HttpClient cliente = new();
        var listJson = await cliente.GetFromJsonAsync<JsonElement>(url+"/productos");
        var elementos = listJson.EnumerateArray().ToArray();
        AvaloniaList<Componente> listaComponentes = new();
        foreach (var elemento in elementos)
        {
            listaComponentes.Add(new Componente{
                Id = elemento.GetProperty("id").GetGuid(),
                Nombre = elemento.GetProperty("nombre").GetString(),
                Descripcion = elemento.GetProperty("descripcion").GetString(),
                Codigo = elemento.GetProperty("codigo").GetString(),
                PrecioVenta = Decimal.Parse(elemento.GetProperty("precio_venta").GetString()),
                Stock = elemento.GetProperty("stock").GetInt32(),
                Activo = elemento.GetProperty("activo").GetBoolean(),
                FechaAlta = elemento.GetProperty("fecha_alta").GetDateTime()
            });
        }
        return  listaComponentes;
    }
    
    
}