using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Manga
    {
        public Manga()
        {
            CategoryMangas = new HashSet<CategoryManga>();
            Chapteres = new HashSet<Chaptere>();
            Comments = new HashSet<Comment>();
            FollowLists = new HashSet<FollowList>();
        }

        public Guid Id { get; set; }
        public long SubId { get; set; }
        public Guid AuthorId { get; set; }
        public int Status { get; set; }
        public int ViewCount { get; set; }
        public int RateCount { get; set; }
        public int Star { get; set; }
        public int FollowCount { get; set; }
        public string Description { get; set; } = null!;
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual ICollection<CategoryManga> CategoryMangas { get; set; }
        public virtual ICollection<Chaptere> Chapteres { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FollowList> FollowLists { get; set; }
    }
}
