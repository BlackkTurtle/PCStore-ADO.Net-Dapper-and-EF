using System;
using System.Collections.Generic;

namespace PCStoreEF.Entities;

public partial class User
{
    public User()
    {
        Comments = new HashSet<Comment>();
        Orders = new HashSet<Order>();
    }
    public int UserId { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Father { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
