using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class AdminLogin
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
