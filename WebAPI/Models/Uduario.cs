using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Uduario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }
}
