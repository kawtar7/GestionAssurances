using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Adresse { get; set; } = null!;

    public string Ville { get; set; } = null!;

    public virtual ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();
}
