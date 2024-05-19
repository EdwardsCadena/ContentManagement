using System;
using System.Collections.Generic;

namespace Proyecto.Core.Entities;

public partial class MultimediaFile
{
    public int FileId { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public string? FilePath { get; set; }

    public int? ArticleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Article? Article { get; set; }
}
