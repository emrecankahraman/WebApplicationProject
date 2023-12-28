using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class SpotifyLogin
{
    public int SpotifyId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
