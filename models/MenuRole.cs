using System;
using System.Collections.Generic;

namespace server.Models;

public partial class MenuRole
{
    public long Id { get; set; }

    public long MenuId { get; set; }

    public long RoleId { get; set; }

    public bool CanView { get; set; }

    public bool CanEdit { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
