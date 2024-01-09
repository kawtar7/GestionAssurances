using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Formule
{
    public int IdFormule { get; set; }

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();

    public virtual ICollection<Couverture> Couvertures { get; set; } = new List<Couverture>();
}
