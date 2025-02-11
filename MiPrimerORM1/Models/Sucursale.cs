using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Sucursale
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
