using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Inventario? Inventario { get; set; }

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();
}
