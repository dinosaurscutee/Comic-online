using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryMangas = new HashSet<CategoryManga>();
        }

        public Guid Id { get; set; }
        public long SubId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CategoryManga> CategoryMangas { get; set; }
    }
}
