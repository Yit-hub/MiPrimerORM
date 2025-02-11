using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string? Puesto { get; set; }

    public decimal? Salario { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
