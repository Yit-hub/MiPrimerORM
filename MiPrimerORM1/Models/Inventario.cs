using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public DateTime UltimaActualizacion { get; set; }

    public virtual Producto? Producto { get; set; }
}
