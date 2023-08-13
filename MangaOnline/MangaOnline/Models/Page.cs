using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Page
    {
        public Guid Id { get; set; }
        public Guid ChapterId { get; set; }
        public string Image { get; set; } = null!;
        public int PageNumber { get; set; }

        public virtual Chaptere Chapter { get; set; } = null!;
    }
}
