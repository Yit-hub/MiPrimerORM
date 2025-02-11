using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Usuario? Usuario { get; set; }
}
