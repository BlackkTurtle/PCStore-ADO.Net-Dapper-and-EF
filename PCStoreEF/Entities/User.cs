using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PCStoreEF.Entities;

public partial class User:IdentityUser<int>
{
    public User()
    {
        Comments = new HashSet<Comment>();
        Orders = new HashSet<Order>();
    }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Father { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
