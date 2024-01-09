using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Correspondant
{
    public int IdCorrespondant { get; set; }

    public string NomCorrespondant { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public virtual ICollection<DossiersSinistre> DossiersSinistres { get; set; } = new List<DossiersSinistre>();
}
