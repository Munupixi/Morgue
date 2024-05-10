using System;
using System.Collections.Generic;

namespace Morgue.Models;

public partial class User
{
    public static User? ActiveUser;
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Role Role { get; set; } = null!;
}
