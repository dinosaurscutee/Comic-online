using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Notification
    {
        public Guid Id { get; set; }
        public long SubId { get; set; }
        public string? Content { get; set; }
        public Guid ChapterId { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }

        public virtual Chaptere Chapter { get; set; } = null!;
    }
}
