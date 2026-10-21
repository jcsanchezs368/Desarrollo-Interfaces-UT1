using System;

namespace ControlAlmacen2.Models;

public class Proveedor
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    

    public override string ToString()
    {
        return Nombre;
    }
}