using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Envio? Envio { get; set; }

    public virtual Factura? Factura { get; set; }
}
