using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public long SubId { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual RoleClaim? RoleClaim { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
