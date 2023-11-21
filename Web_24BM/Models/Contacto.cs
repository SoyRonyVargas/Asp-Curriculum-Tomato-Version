﻿using System;
using System.Collections.Generic;

namespace Web_24BM.Models;

public partial class Contacto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }
    public string? TituloLaboral { get; set; }
    public string? Objetivos { get; set; }

    public string? Email { get; set; }

    public string Telefono { get; set; }

    public string SitioWeb { get; set; }

    public List<Habilidad> Habilidades { get; set; }
    public List<Experiencia> Experiencia { get; set; }
    public List<Educacion> Educacion { get; set; }

    public string? Foto { get; set; }

}
