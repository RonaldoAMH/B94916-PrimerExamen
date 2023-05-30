using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public int Sexo { get; set; }

    public DateTime? FechaDeNacimiento { get; set; }

    public string CedulaMadre { get; set; } = null!;

    public string CedulaPadre { get; set; } = null!;

}
