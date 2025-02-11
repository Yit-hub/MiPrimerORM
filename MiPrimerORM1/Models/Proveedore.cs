using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Proveedore
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Contacto { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
