using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Contrat
{
    public int IdContrat { get; set; }

    public DateTime DateSouscription { get; set; }

    public DateTime DateEcheance { get; set; }

    public int IdClient { get; set; }

    public int IdFormule { get; set; }

    public virtual ICollection<DossiersSinistre> DossiersSinistres { get; set; } = new List<DossiersSinistre>();

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Formule IdFormuleNavigation { get; set; } = null!;
}
