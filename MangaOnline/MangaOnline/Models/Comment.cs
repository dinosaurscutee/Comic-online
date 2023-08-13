using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Comment
    {
        public Guid Id { get; set; }
        public Guid MangaId { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int LikedCount { get; set; }
        public int DislikedCount { get; set; }
        public bool IsActive { get; set; }

        public virtual Manga Manga { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
