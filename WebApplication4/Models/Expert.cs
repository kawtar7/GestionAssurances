using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Expert
{
    public int IdExpert { get; set; }

    public string NomExpert { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public virtual ICollection<DossiersSinistre> DossiersSinistres { get; set; } = new List<DossiersSinistre>();
}
