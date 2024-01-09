using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class DossiersSinistre
{
    public int IdDossierSinistre { get; set; }

    public DateTime DateCouverture { get; set; }

    public DateTime DateCloture { get; set; }

    public double Indemnites { get; set; }

    public int IdCorrespondant { get; set; }

    public int IdExpert { get; set; }

    public int IdContrat { get; set; }

    public virtual Contrat IdContratNavigation { get; set; } = null!;

    public virtual Correspondant IdCorrespondantNavigation { get; set; } = null!;

    public virtual Expert IdExpertNavigation { get; set; } = null!;

    public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
}
