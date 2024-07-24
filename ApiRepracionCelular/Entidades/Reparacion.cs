﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ApiRepracionCelular.Entidades;

public partial class Reparacion
{
    public int Id { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string Estado { get; set; }

    public int? IdCliente { get; set; }

    public int? IdTecnico { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; }

    public virtual Usuario IdTecnicoNavigation { get; set; }

    public virtual ICollection<ReparacionDetalle> ReparacionDetalle { get; set; } = new List<ReparacionDetalle>();
}