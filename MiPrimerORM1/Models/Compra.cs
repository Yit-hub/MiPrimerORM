using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int? ProveedorId { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual Proveedore? Proveedor { get; set; }
}
