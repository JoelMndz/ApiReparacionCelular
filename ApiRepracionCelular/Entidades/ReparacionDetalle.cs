﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ApiRepracionCelular.Entidades;

public partial class ReparacionDetalle
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? IdReparacion { get; set; }

    public virtual Reparacion IdReparacionNavigation { get; set; }
}