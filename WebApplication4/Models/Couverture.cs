using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Couverture
{
    public int IdFormule { get; set; }

    public int IdGarantie { get; set; }

    public double Plafond { get; set; }

    public double Franchise { get; set; }

    public virtual Formule IdFormuleNavigation { get; set; } = null!;

    public virtual Garanty IdGarantieNavigation { get; set; } = null!;
}
