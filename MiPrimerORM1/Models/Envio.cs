using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Envio
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public int? SucursalId { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Sucursale? Sucursal { get; set; }
}
