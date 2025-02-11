using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
