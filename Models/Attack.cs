using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class Attack
{
    public int AttackId { get; set; }

    public string? Type { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<SentEmail> SentEmails { get; set; } = new List<SentEmail>();
}
