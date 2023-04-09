using System;
using System.Collections.Generic;

namespace PCStoreEF.Entities;

public partial class Product
{
    public Product()
    {
        Comments = new HashSet<Comment>();
        PartOrders = new HashSet<PartOrder>();
    }
    public int Article { get; set; }

    public string Name { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public int Type { get; set; }

    public float Price { get; set; }

    public string? ProductInfo { get; set; }

    public int BrandId { get; set; }

    public bool Availability { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<PartOrder> PartOrders { get; } = new List<PartOrder>();

    public virtual Types TypeNavigation { get; set; } = null!;
}
