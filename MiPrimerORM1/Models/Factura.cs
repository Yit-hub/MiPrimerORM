using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Factura
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public DateTime? FechaEmision { get; set; }

    public decimal? Total { get; set; }

    public virtual Pago? Pago { get; set; }

    public virtual Pedido? Pedido { get; set; }
}
