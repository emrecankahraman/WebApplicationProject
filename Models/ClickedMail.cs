using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class ClickedMail
{
    public int ClickId { get; set; }

    public int EmailId { get; set; }

    public DateTime Date { get; set; }

    public bool Success { get; set; }

    public virtual SentEmail Email { get; set; } = null!;
}
