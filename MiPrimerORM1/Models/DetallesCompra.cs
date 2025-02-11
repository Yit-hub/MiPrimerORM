using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class DetallesCompra
{
    public int Id { get; set; }

    public int? CompraId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public virtual Compra? Compra { get; set; }

    public virtual Producto? Producto { get; set; }
}
