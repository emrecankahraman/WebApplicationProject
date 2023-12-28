using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class Victim
{
    public int VictimId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<SentEmail> SentEmails { get; set; } = new List<SentEmail>();
}
