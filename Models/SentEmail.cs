using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class SentEmail
{
    public int EmailId { get; set; }

    public int VictimId { get; set; }

    public int AttackId { get; set; }

    public DateTime SentDate { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual Attack Attack { get; set; } = null!;

    public virtual ICollection<ClickedMail> ClickedMails { get; set; } = new List<ClickedMail>();

    public virtual Victim Victim { get; set; } = null!;
}
