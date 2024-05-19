using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.DTOs
{
    public class ArticleDTOs
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int? UserId { get; set; }
    }
}
