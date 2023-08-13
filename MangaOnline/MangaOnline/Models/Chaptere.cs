using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Chaptere
    {
        public Chaptere()
        {
            Notifications = new HashSet<Notification>();
            Pages = new HashSet<Page>();
        }

        public Guid Id { get; set; }
        public long SubId { get; set; }
        public Guid MangaId { get; set; }
        public int ChapterNumber { get; set; }
        public string Name { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string? FilePdf { get; set; }

        public virtual Manga Manga { get; set; } = null!;
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
