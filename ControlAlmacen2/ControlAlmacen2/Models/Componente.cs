using System;
using System.Text.Json.Serialization;

namespace ControlAlmacen2.Models;

public class Componente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Codigo { get; set; }
    [JsonPropertyName("precio_venta")]
    public decimal PrecioVenta { get; set; }
    public int Stock { get; set; }
    public bool Activo { get; set; } = true;
    [JsonPropertyName("fecha_alta")]
    public DateTime FechaAlta { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return Nombre;
    }
}