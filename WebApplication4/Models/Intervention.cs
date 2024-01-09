using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Intervention
{
    public int IdDossierIntervention { get; set; }

    public DateTime DateIntervention { get; set; }

    public virtual DossiersSinistre IdDossierInterventionNavigation { get; set; } = null!;
}
