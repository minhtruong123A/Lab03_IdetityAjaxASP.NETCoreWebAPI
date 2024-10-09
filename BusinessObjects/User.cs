using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}
