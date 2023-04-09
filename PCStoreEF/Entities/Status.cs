using System;
using System.Collections.Generic;

namespace PCStoreEF.Entities;

public partial class Status
{
    public Status()
    {
        Orders = new HashSet<Order>();
    }
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
