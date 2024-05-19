using System;
using System.Collections.Generic;

namespace Proyecto.Core.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Role? Role { get; set; }
}
