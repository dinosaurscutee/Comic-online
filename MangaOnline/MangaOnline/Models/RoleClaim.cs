using System;
using System.Collections.Generic;

namespace MangaOnline.Models
{
    public partial class RoleClaim
    {
        public long Id { get; set; }
        public Guid RoleId { get; set; }
        public string? ClaimValue { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
