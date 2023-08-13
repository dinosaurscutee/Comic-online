using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Author
    {
        public Author()
        {
            Mangas = new HashSet<Manga>();
        }

        public Guid Id { get; set; }
        public long SubId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Manga> Mangas { get; set; }
    }
}
