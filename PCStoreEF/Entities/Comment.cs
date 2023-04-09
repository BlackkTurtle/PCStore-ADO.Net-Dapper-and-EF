using System;
using System.Collections.Generic;

namespace PCStoreEF.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int Article { get; set; }

    public int Stars { get; set; }

    public DateTime? CommentDate { get; set; }

    public int UserId { get; set; }

    public string? Comment1 { get; set; }

    public virtual Product ArticleNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
