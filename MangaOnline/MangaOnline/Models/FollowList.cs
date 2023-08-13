using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class FollowList
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MangaId { get; set; }

        public virtual Manga Manga { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
