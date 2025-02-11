using System;
using System.Collections.Generic;

namespace MiPrimerORM1.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Rol { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Empleado? Empleado { get; set; }
}
