using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class CategoryManga
    {
        public Guid CategoryId { get; set; }
        public Guid MangaId { get; set; }
        public long SubId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Manga Manga { get; set; } = null!;
    }
}
