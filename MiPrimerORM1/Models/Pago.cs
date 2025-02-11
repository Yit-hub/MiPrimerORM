using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int? FacturaId { get; set; }

    public int? MetodoPagoId { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual MetodosPago? MetodoPago { get; set; }
}
