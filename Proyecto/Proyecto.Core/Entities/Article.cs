using System;
using System.Collections.Generic;

namespace Proyecto.Core.Entities;

public partial class Article
{
    public int ArticleId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<MultimediaFile> MultimediaFiles { get; set; } = new List<MultimediaFile>();

    public virtual User? User { get; set; }
}
