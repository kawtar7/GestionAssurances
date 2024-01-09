using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Garanty
{
    public int IdGarantie { get; set; }

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Couverture> Couvertures { get; set; } = new List<Couverture>();
}
